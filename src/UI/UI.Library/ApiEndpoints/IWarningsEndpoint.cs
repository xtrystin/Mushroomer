﻿using UI.ApiLibrary.Dto;

namespace UI.ApiLibrary.ApiEndpoints;

public interface IWarningsEndpoint
{
    Task Add(WarningDto warning);
    Task Delete(Guid id);
    Task<WarningDto> Get(string id);
    Task<List<WarningDto>> GetAll();
    Task Update(WarningDto warning);
    Task<bool?> GetReactionForUser(Guid warningId);
    public Task PostReaction(Guid warningId, bool approve);
}
