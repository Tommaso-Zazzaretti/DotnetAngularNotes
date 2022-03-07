using AutoMapper;
using DotNet6Mediator.ApplicationLayer.Helpers.Dto;
using DotNet6Mediator.ApplicationLayer.Mediators.UserMediator.Commands;
using DotNet6Mediator.DomainLayer.Entities;
using DotNet6Mediator.InfrastructureLayer.Services.Interfaces;
using MediatR;

namespace DotNet6Mediator.ApplicationLayer.Mediators.UserMediator.Handlers
{
    public class DeleteUserHandler:IRequestHandler<DeleteUserCommand,UserDtoResponse?>
    {
        private readonly IMapper _mapper;
        private readonly ICrudService<User> _service;

        public DeleteUserHandler(IMapper Mapper, ICrudService<User> Service)
        {
            this._mapper = Mapper;
            this._service = Service;
        }

        public async Task<UserDtoResponse?> Handle(DeleteUserCommand Command,CancellationToken CancellationToken)
        {
            int UserId = Command.UserId;
            User? DeletedModel = await this._service.Delete(UserId);
            //Return a DTO
            UserDtoResponse DeletedUserDto = this._mapper.Map<User, UserDtoResponse>(DeletedModel!);
            return DeletedUserDto;
        }
    }
}
