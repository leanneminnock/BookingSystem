using AutoMapper;
using BookingSystem.Data.Entities;
using BookingSystem.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BookingSystem.Data
{
    public class BookingMappingProfile : Profile
    {
      public BookingMappingProfile()
        {
            CreateMap<Booking, BookingViewModel>().ReverseMap();
            CreateMap<Hotel, HotelViewModel>().ReverseMap();
            CreateMap<Room, RoomViewModel>().ReverseMap();
            CreateMap<Customer, CustomerViewModel>().ReverseMap();
        }
    }
}
