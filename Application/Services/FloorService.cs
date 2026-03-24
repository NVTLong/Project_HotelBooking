using AutoMapper;
using Project_HotelBooking.Application.DTOs.Floor;
using Project_HotelBooking.Application.Interfaces.Repository;
using Project_HotelBooking.Application.Interfaces.Service;
using Project_HotelBooking.Models;

namespace Project_HotelBooking.Application.Services
{
    public class FloorService : IFloorService
    {
        private readonly IFloorRepository _floorRepository;
        private readonly IMapper _mapper;
        public FloorService(IFloorRepository floorRepository, IMapper mapper)
        {
            _floorRepository = floorRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<FloorDto>> GetAllAsync()
        {
            var floor = await _floorRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<FloorDto>>(floor);
        }

        public async Task<FloorDto> GetByIdAsync(int id)
        {
            var floor = await _floorRepository.GetByIdAsync(id);

            if (floor == null)
            {
                return null;
            }

            return _mapper.Map<FloorDto>(floor);
        }
        public async Task CreateAsync(CreateFloorDto createFloorDto)
        {
            var floor = _mapper.Map<Floor>(createFloorDto);

            await _floorRepository.CreateAsync(floor);
        }

        public async Task UpdateAsync(UpdateFloorDto updateFloorDto)
        {
            var floor = await _floorRepository.GetByIdAsync(updateFloorDto.Id);

            if(floor == null)
            {
                throw new Exception("Không tồn tại phòng");
            }
            _mapper.Map(updateFloorDto, floor);
            await _floorRepository.UpdateAsync(floor);
        }
        public async Task DeleteAsync(int id)
        {
            var floor = await _floorRepository.GetByIdAsync(id);

            if(floor != null)
            {
                await _floorRepository.DeleteAsync(id);
                await _floorRepository.SaveAsync();
            }
        }
    }
}
