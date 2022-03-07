using AutoMapper;
using DotNet6Mediator.ApplicationLayer.Mediators.UserMediator.Commands;
using DotNet6Mediator.ApplicationLayer.Helpers.Dto;
using DotNet6Mediator.DomainLayer.Entities;
using MediatR;
using DotNet6Mediator.InfrastructureLayer.Services.Interfaces;

namespace DotNet6Mediator.ApplicationLayer.Mediators.UserMediator.Handlers
{
    public class UpdateUserHandler : IRequestHandler<UpdateUserCommand, UserDtoResponse?>
    {
        private readonly IMapper _mapper;
        private readonly ICrudService<User> _service;

        public UpdateUserHandler(IMapper Mapper, ICrudService<User> Service)
        {
            this._mapper = Mapper;
            this._service = Service;
        }

        public async Task<UserDtoResponse?> Handle(UpdateUserCommand Command, CancellationToken CancellationToken)
        {
            UserDtoRequest UpdateUserDto = Command.UserToUpdate;
            //Convert Input Dto to Model Object:
            User? UserToUpdate = this._mapper.Map<UserDtoRequest, User>(UpdateUserDto);
            User? UpdatedUser = await this._service.Update(UserToUpdate);
            //Return a DTO
            UserDtoResponse UpdatedUserDto = this._mapper.Map<User, UserDtoResponse>(UpdatedUser!);
            return UpdatedUserDto;
        }
    }
}
