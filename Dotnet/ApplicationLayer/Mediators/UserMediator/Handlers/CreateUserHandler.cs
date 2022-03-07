using AutoMapper;
using DotNet6Mediator.ApplicationLayer.Helpers.Dto;
using DotNet6Mediator.ApplicationLayer.Mediators.UserMediator.Commands;
using DotNet6Mediator.DomainLayer.Entities;
using DotNet6Mediator.InfrastructureLayer.Services.Interfaces;
using MediatR;

namespace DotNet6Mediator.ApplicationLayer.Mediators.UserMediator.Handlers
{
    public class CreateUserHandler:IRequestHandler<CreateUserCommand,UserDtoResponse?>
    {
        private readonly IMapper _mapper;
        private readonly ICrudService<User> _service;

        public CreateUserHandler(IMapper Mapper, ICrudService<User> Service)
        {
            this._mapper = Mapper;
            this._service = Service;
        }
        public async Task<UserDtoResponse?> Handle(CreateUserCommand Command,CancellationToken CancellationToken)
        {
            UserDtoRequest NewUserDto = Command.UserToAdd;
            //Convert Input Dto to Model Object:
            User? UserToAdd = this._mapper.Map<UserDtoRequest,User>(NewUserDto);
            User? AddedUser = await this._service.Create(UserToAdd);
            //Return a DTO
            UserDtoResponse AddedUserDto = this._mapper.Map<User, UserDtoResponse>(AddedUser!);
            return AddedUserDto;
        }
    }
}
