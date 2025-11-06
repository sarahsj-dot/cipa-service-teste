using ClosedXML.Excel;
using Microsoft.Extensions.Localization;
using TIVIT.CIPA.Api.Domain.Interfaces.Business;
using TIVIT.CIPA.Api.Domain.Interfaces.Models;
using TIVIT.CIPA.Api.Domain.Interfaces.Repositories;
using TIVIT.CIPA.Api.Domain.Model;
using TIVIT.CIPA.Api.Domain.Model.Requests;
using TIVIT.CIPA.Api.Domain.Model.Responses;
using TIVIT.CIPA.Api.Domain.Resources;
using TIVIT.CIPA.Api.Domain.Validators;

namespace TIVIT.CIPA.Api.Domain.Business
{
  
    public class VoterBusiness : IVoterBusiness
    {
        private readonly IUserInfo _userInfo;
        private readonly IVoterRepository _voterRepository;
        private readonly IStringLocalizer<SharedResource> _localizer;

        public VoterBusiness(
            IUserInfo userInfo,
            IVoterRepository voterRepository,
            IStringLocalizer<SharedResource> localizer)
        {
            _userInfo = userInfo;
            _voterRepository = voterRepository;
            _localizer = localizer;
        }

        public async Task<Response<VoterDetailResponse>> GetByIdAsync(int id)
        {
            var response = new Response<VoterDetailResponse>();

            var voter = await this._voterRepository.GetByIdAsync(id);

            if (voter == null)
                return response;

            response.Data = new VoterDetailResponse()
            {
                Id = voter.Id,
                ElectionId = voter.ElectionId,
                CompanyId = voter.CompanyId,
                ProfileId = voter.ProfileId,
                Registry = voter.Registry,
                Name = voter.Name,
                JobTitle = voter.JobTitle,
                Email = voter.Email,
                CorporateEmail = voter.CorporateEmail,
                ContactEmail = voter.ContactEmail,
                CorporatePhone = voter.CorporatePhone,
                ContactPhone = voter.ContactPhone,
                Site = voter.Site,
                Department = voter.Department,
                Token = voter.Token,
                HasVoted = voter.HasVoted,
                IsActive = voter.IsActive
            };

            return response;
        }

        public async Task<Response<IEnumerable<VoterResumeResponse>>> GetByElectionIdAsync(int electionId)
        {
            var response = new Response<IEnumerable<VoterResumeResponse>>();

            var voters = await this._voterRepository.GetByElectionIdAsync(electionId);

            if (voters == null)
                return response;

            response.Data = voters.Select(x => new VoterResumeResponse()
            {
                Id = x.Id,
                ElectionId = x.ElectionId,
                Registry = x.Registry,
                Name = x.Name,
                JobTitle = x.JobTitle,
                Email = x.Email,
                HasVoted = x.HasVoted
            });

            return response;
        }

        public async Task<Response<int>> CreateAsync(VoterCreateRequest createRequest)
        {
            var response = new Response<int>();

            var validator = new VoterValidator(_localizer);
            validator.ValidateCreate(createRequest);

            if (!validator.IsValid)
            {
                response.AddMessage(validator.Notifications.Select(x => x.Message));
                return response;
            }

            var voter = new Voter()
            {
                ElectionId = createRequest.ElectionId,
                CompanyId = createRequest.CompanyId,
                ProfileId = createRequest.ProfileId,
                Registry = createRequest.Registry,
                Name = createRequest.Name,
                JobTitle = createRequest.JobTitle,
                Email = createRequest.Email,
                CorporateEmail = createRequest.CorporateEmail,
                ContactEmail = createRequest.ContactEmail,
                CorporatePhone = createRequest.CorporatePhone,
                ContactPhone = createRequest.ContactPhone,
                Site = createRequest.Site,
                Department = createRequest.Department,
                Token = GenerateUniqueToken(),
                HasVoted = false,
                IsActive = true,
                CreateDate = DateTime.Now,
                CreateUser = _userInfo.Upn
            };

            await _voterRepository.CreateAsync(voter);

            response.Data = voter.Id;

            return response;
        }

        public async Task<Response> UpdateAsync(int id, VoterUpdateRequest updateRequest)
        {
            var response = new Response();

            var validator = new VoterValidator(_localizer);
            validator.ValidateUpdate(id, updateRequest);

            if (!validator.IsValid)
            {
                response.AddMessage(validator.Notifications.Select(x => x.Message));
                return response;
            }

            var voter = await this._voterRepository.GetByIdAsync(id);

            if (voter == null)
            {
                response.AddMessage("Eleitor não encontrado.");
                return response;
            }

            voter.ElectionId = updateRequest.ElectionId ?? voter.ElectionId;
            voter.CompanyId = updateRequest.CompanyId ?? voter.CompanyId;
            voter.ProfileId = updateRequest.ProfileId ?? voter.ProfileId;
            voter.Registry = updateRequest.Registry ?? voter.Registry;
            voter.Name = updateRequest.Name ?? voter.Name;
            voter.JobTitle = updateRequest.JobTitle ?? voter.JobTitle;
            voter.Email = updateRequest.Email ?? voter.Email;
            voter.CorporateEmail = updateRequest.CorporateEmail ?? voter.CorporateEmail;
            voter.ContactEmail = updateRequest.ContactEmail ?? voter.ContactEmail;
            voter.CorporatePhone = updateRequest.CorporatePhone ?? voter.CorporatePhone;
            voter.ContactPhone = updateRequest.ContactPhone ?? voter.ContactPhone;
            voter.Site = updateRequest.Site ?? voter.Site;
            voter.Department = updateRequest.Department ?? voter.Department;
            voter.UpdateDate = DateTime.Now;
            voter.UpdateUser = _userInfo.Upn;

            await _voterRepository.UpdateAsync(voter);

            return response;
        }

        public async Task<Response> ChangeActiveAsync(bool isActive, int id)
        {
            var response = new Response();

            var voter = await this._voterRepository.GetByIdAsync(id);

            if (voter == null)
            {
                response.AddMessage("Eleitor não encontrado.");
                return response;
            }

            voter.IsActive = isActive;
            voter.UpdateDate = DateTime.Now;
            voter.UpdateUser = _userInfo.Upn;

            await this._voterRepository.UpdateAsync(voter);

            return response;
        }

        public Response<byte[]> GetSyncTemplate()
        {
            var response = new Response<byte[]>();

            try
            {
                var filepath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Templates\\Example", "sync-voters-template.xlsx");

                using (var workbook = new XLWorkbook(filepath))
                {
                    using (var stream = new MemoryStream())
                    {
                        workbook.SaveAs(stream);
                        response.Data = stream.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                response.AddMessage($"Erro ao obter template: {ex.Message}");
            }

            return response;
        }

        public async Task<Response<int>> SyncByFileAsync(MemoryStream syncFile, int electionId, int companyId)
        {
            var response = new Response<int>();

            try
            {

                if (syncFile == null || syncFile.Length == 0)
                {
                    response.AddMessage("Arquivo não fornecido ou vazio.");
                    return response;
                }

                if (electionId <= 0)
                {
                    response.AddMessage("ID da eleição inválido.");
                    return response;
                }

                if (companyId <= 0)
                {
                    response.AddMessage("ID da empresa inválido.");
                    return response;
                }

                using (var workbook = new XLWorkbook(syncFile))
                {
                    var worksheet = workbook.Worksheet(1);
                    int rowCount = worksheet.LastRowUsed()?.RowNumber() ?? 0;

                    if (rowCount < 2)
                    {
                        response.AddMessage("Arquivo vazio ou sem dados de eleitor.");
                        return response;
                    }

                    
                    var voters = new List<Voter>();

                    for (int row = 2; row <= rowCount; row++)
                    {
                        // Pular linhas vazias
                        var registryCell = worksheet.Cell(row, 1).GetString();
                        if (string.IsNullOrWhiteSpace(registryCell))
                            continue;

                        var voter = new Voter()
                        {
                            ElectionId = electionId,
                            CompanyId = companyId,
                            Registry = registryCell.Trim(),
                            Name = worksheet.Cell(row, 2).GetString()?.Trim(),
                            JobTitle = worksheet.Cell(row, 3).GetString()?.Trim(),
                            Email = worksheet.Cell(row, 4).GetString()?.Trim(),
                            CorporateEmail = worksheet.Cell(row, 5).GetString()?.Trim(),
                            ContactEmail = worksheet.Cell(row, 6).GetString()?.Trim(),
                            CorporatePhone = worksheet.Cell(row, 7).GetString()?.Trim(),
                            ContactPhone = worksheet.Cell(row, 8).GetString()?.Trim(),
                            Site = worksheet.Cell(row, 9).GetString()?.Trim(),
                            Department = worksheet.Cell(row, 10).GetString()?.Trim(),
                            Token = GenerateUniqueToken(),
                            HasVoted = false,
                            IsActive = true,
                            CreateDate = DateTime.Now,
                            CreateUser = _userInfo.Upn
                        };

                        
                        var validationResult = ValidateVoterRow(voter, row);
                        if (!string.IsNullOrEmpty(validationResult))
                        {
                            response.AddMessage(validationResult);
                            return response;
                        }

                        voters.Add(voter);
                    }

                    if (voters.Count == 0)
                    {
                        response.AddMessage("Nenhum eleitor válido encontrado no arquivo.");
                        return response;
                    }

                    
                    await _voterRepository.CreateRangeAsync(voters);
                    response.Data = voters.Count;
                }
            }
            catch (InvalidOperationException ex)
            {
                response.AddMessage($"Erro ao processar arquivo Excel: {ex.Message}");
            }
            catch (Exception ex)
            {
                response.AddMessage($"Erro inesperado ao sincronizar arquivo: {ex.Message}");
            }

            return response;
        }

        #region "Private Methods"

        private string GenerateUniqueToken()
        {
            return Guid.NewGuid().ToString("N").Substring(0, 32);
        }

        private string ValidateVoterRow(Voter voter, int rowNumber)
        {
            if (string.IsNullOrWhiteSpace(voter.Email))
                return $"Linha {rowNumber}: Email é obrigatório.";

            if (string.IsNullOrWhiteSpace(voter.Name))
                return $"Linha {rowNumber}: Nome é obrigatório.";

            if (string.IsNullOrWhiteSpace(voter.Registry))
                return $"Linha {rowNumber}: Matrícula é obrigatória.";

            // Validar formato de email
            if (!IsValidEmail(voter.Email))
                return $"Linha {rowNumber}: Email inválido.";

            return null;
        }

        /// <summary>
        /// Valida formato de email
        /// </summary>
        private bool IsValidEmail(string email)
        {
            try
            {
                var addr = new System.Net.Mail.MailAddress(email);
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        #endregion
    }
}