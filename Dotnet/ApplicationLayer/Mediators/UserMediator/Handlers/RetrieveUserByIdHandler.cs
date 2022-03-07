using DotNet6Mediator.ApplicationLayer.Mediators.UserMediators.Queries;
using DotNet6Mediator.ApplicationLayer.Helpers.Dto;
using MediatR;
using AutoMapper;
using DotNet6Mediator.DomainLayer.Entities;
using DotNet6Mediator.InfrastructureLayer.Services.Interfaces;

namespace DotNet6Mediator.ApplicationLayer.Mediators.UserMediators.Handlers
{
    public class RetrieveUserByIdHandler:IRequestHandler<RetrieveUserByIdQuery,UserDtoResponse?>
    {
        private readonly IMapper _mapper;
        private readonly ICrudService<User> _service;

        public RetrieveUserByIdHandler(IMapper Mapper, ICrudService<User> Service)
        {
            this._mapper = Mapper;
            this._service = Service;
        }
        public async Task<UserDtoResponse?> Handle(RetrieveUserByIdQuery Request, CancellationToken CancellationToken)
        {
            int UserId = Request.UserId;
            User? UserModel = await this._service.Retrieve(UserId);
            return this._mapper.Map<User, UserDtoResponse>(UserModel!);
        }
    }
}
