﻿using UI.ApiLibrary.Dto;

namespace UI.ApiLibrary.ApiEndpoints;

public interface IWarningsEndpoint
{
    Task Add(WarningDto warning);
    Task Delete(Guid id);
    Task<WarningDto> Get(string id);
    Task<List<WarningDto>> GetAll(bool onlyInactive = false, bool onlyInactiveForUser = false);
    Task Update(WarningDto warning);
    Task<bool?> GetReactionForUser(Guid warningId, Guid userId);
    public Task PostReaction(Guid warningId, bool approve);
    Task ChangeStatus(Guid id, bool changeToActive);
}
