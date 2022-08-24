﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;

using BoardgamesEShopManagement.Domain.Entities;
using BoardgamesEShopManagement.Application.Abstract.RepositoryInterfaces;
using BoardgamesEShopManagement.Application.Abstract;

namespace BoardgamesEShopManagement.Application.Boardgames.Queries.GetBoardgamesList
{
    public class GetBoardgameListQueryHandler : IRequestHandler<GetBoardgamesListQuery, List<Boardgame>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetBoardgameListQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<Boardgame>> Handle(GetBoardgamesListQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.BoardgameRepository.GetAll();
        }
    }
}
