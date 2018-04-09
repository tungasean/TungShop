using System.Collections.Generic;
using TungShop.Data.Infrastructure;
using TungShop.Data.Repositories;
using TungShop.Model.Models;

namespace TungShop.Service
{
    public interface IRoomService
    {
        Room Add(Room Room);

        void Update(Room Room);

        Room Delete(int id);

        IEnumerable<Room> GetAll();

        IEnumerable<Room> GetAll(string keyword);

        Room GetById(int id);

        void Save();
    }

    public class RoomService : IRoomService
    {
        private IRoomRepository _RoomRepository;
        private IUnitOfWork _unitOfWork;

        public RoomService(IRoomRepository RoomRepository, IUnitOfWork unitOfWork)
        {
            this._RoomRepository = RoomRepository;
            this._unitOfWork = unitOfWork;
        }

        public Room Add(Room Room)
        {
            return _RoomRepository.Add(Room);
        }

        public Room Delete(int id)
        {
            return _RoomRepository.Delete(id);
        }

        public IEnumerable<Room> GetAll()
        {
            return _RoomRepository.GetAll();
        }

        public IEnumerable<Room> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _RoomRepository.GetMulti(x => x.RoomName.Contains(keyword));
            else
                return _RoomRepository.GetAll();

        }

        public Room GetById(int id)
        {
            return _RoomRepository.GetSingleById(id);
        }

        public void Save()
        {
            _unitOfWork.Commit();
        }

        public void Update(Room Room)
        {
            _RoomRepository.Update(Room);
        }
    }
}