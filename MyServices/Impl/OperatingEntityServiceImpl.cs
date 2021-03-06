﻿using AutoMapper;
using HaloBiz.DTOs.ApiDTOs;
using HaloBiz.DTOs.ReceivingDTOs;
using HaloBiz.Repository;
using HaloBiz.Model;
using HaloBiz.DTOs.TransferDTOs;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HaloBiz.MyServices.Impl
{
    public class OperatingEntityServiceImpl : IOperatingEntityService
    {
        private readonly ILogger<OperatingEntityServiceImpl> _logger;
        private readonly IOperatingEntityRepository _operatingEntityRepo;
        private readonly IMapper _mapper;

        public OperatingEntityServiceImpl(IOperatingEntityRepository operationgEntityRepo, ILogger<OperatingEntityServiceImpl> logger, IMapper mapper)
        {
            this._mapper = mapper;
            this._operatingEntityRepo = operationgEntityRepo;
            this._logger = logger;
        }

        public async Task<ApiResponse> AddOperatingEntity(OperatingEntityReceivingDTO operatingEntityReceivingDTO)
        {
            var operatingEntity = _mapper.Map<OperatingEntity>(operatingEntityReceivingDTO);
            var savedOperatingEntity = await _operatingEntityRepo.SaveOperatingEntity(operatingEntity);
            if (savedOperatingEntity == null)
            {
                return new ApiResponse(500);
            }
            var operatingEntityTransferDTOs = _mapper.Map<OperatingEntityTransferDTO>(operatingEntity);
            return new ApiOkResponse(operatingEntityTransferDTOs);
        }

        public async Task<ApiResponse> GetAllOperatingEntities()
        {
            var operatingEntity = await _operatingEntityRepo.FindAllOperatingEntity();
            if (operatingEntity == null)
            {
                return new ApiResponse(404);
            }
            var operatingEntityTransferDTOs = _mapper.Map<IEnumerable<OperatingEntityTransferDTO>>(operatingEntity);
            return new ApiOkResponse(operatingEntityTransferDTOs);
        }

        public async Task<ApiResponse> GetOperatingEntityById(long id)
        {
            var operatingEntity = await _operatingEntityRepo.FindOperatingEntityById(id);
            if (operatingEntity == null)
            {
                return new ApiResponse(404);
            }
            var operatingEntityTransferDTOs = _mapper.Map<OperatingEntityTransferDTO>(operatingEntity);
            return new ApiOkResponse(operatingEntityTransferDTOs);
        }

        public async Task<ApiResponse> GetOperatingEntityByName(string name)
        {
            var operatingEntity = await _operatingEntityRepo.FindOperatingEntityByName(name);
            if (operatingEntity == null)
            {
                return new ApiResponse(404);
            }
            var operatingEntityTransferDTOs = _mapper.Map<OperatingEntityTransferDTO>(operatingEntity);
            return new ApiOkResponse(operatingEntityTransferDTOs);
        }

        public async Task<ApiResponse> UpdateOperatingEntity(long id, OperatingEntityReceivingDTO operatingEntityReceivingDTO)
        {
            var operatingEntityToUpdate = await _operatingEntityRepo.FindOperatingEntityById(id);
            if (operatingEntityToUpdate == null)
            {
                return new ApiResponse(404);
            }
            operatingEntityToUpdate.Name = operatingEntityReceivingDTO.Name;
            operatingEntityToUpdate.Description = operatingEntityReceivingDTO.Description;
            operatingEntityToUpdate.HeadId = operatingEntityReceivingDTO.HeadId;
            operatingEntityToUpdate.DivisionId = operatingEntityReceivingDTO.DivisionId;

            var updatedOperatingEntity = await _operatingEntityRepo.UpdateOperatingEntity(operatingEntityToUpdate);

            if (updatedOperatingEntity == null)
            {
                return new ApiResponse(500);
            }
            var operatingEntityTransferDTOs = _mapper.Map<OperatingEntityTransferDTO>(updatedOperatingEntity);
            return new ApiOkResponse(operatingEntityTransferDTOs);


        }

        public async Task<ApiResponse> DeleteOperatingEntity(long id)
        {
            var operatingEntityToDelete = await _operatingEntityRepo.FindOperatingEntityById(id);
            if (operatingEntityToDelete == null)
            {
                return new ApiResponse(404);
            }

            if (!await _operatingEntityRepo.DeleteOperatingEntity(operatingEntityToDelete))
            {
                return new ApiResponse(500);
            }

            return new ApiOkResponse(true);
        }

    }
}
