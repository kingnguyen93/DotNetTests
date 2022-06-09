using AutoMapper;
using DotNetTests.Domain.Entities;
using DotNetTests.Infrastructure.Repositories;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotNetTests.Application.Commands.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, User>
    {
        private readonly IMapper mapper;
        private readonly IGenericRepository<User, Guid> userRepository;

        public CreateUserCommandHandler(IMapper mapper, IGenericRepository<User, Guid> userRepository)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
        }

        public async Task<User> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var user = mapper.Map<User>(request);
            await userRepository.AddAsync(user);
            await userRepository.UnitOfWork.SaveChangesAsync(cancellationToken);
            return user;
        }
    }
}
