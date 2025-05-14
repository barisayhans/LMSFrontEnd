using AutoMapper;
using CleanArchitecture.Core.DTOs.Book;
using CleanArchitecture.Core.DTOs.BorrowTransaction;
using CleanArchitecture.Core.DTOs.Fine;
using CleanArchitecture.Core.DTOs.Notification;
using CleanArchitecture.Core.DTOs.Reservation;
using CleanArchitecture.Core.DTOs.User;
using CleanArchitecture.Core.DTOs.Waitlist;
using CleanArchitecture.Core.Entities;
using CleanArchitecture.Core.Features.Categories.Queries.GetAllCategories;
using CleanArchitecture.Core.Features.Products.Commands.CreateProduct;
using CleanArchitecture.Core.Features.Products.Queries.GetAllProducts;

namespace CleanArchitecture.Core.Mappings
{
    public class GeneralProfile : Profile
    {
        public GeneralProfile()
        {
            CreateMap<Product, GetAllProductsViewModel>().ReverseMap();
            CreateMap<CreateProductCommand, Product>();
            CreateMap<GetAllProductsQuery, GetAllProductsParameter>();
            CreateMap<GetAllCategoriesQuery, GetAllCategoriesParameter>();
            CreateMap<Category, GetAllCategoriesViewModel>().ReverseMap();
            CreateMap<Book, BookDto>().ReverseMap().
                ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));
            CreateMap<CreateBookDto, Book>().ReverseMap();
            CreateMap<Borrow, BorrowTransactionDto>().ReverseMap();
            CreateMap<Fine, FineDto>().ReverseMap();
            CreateMap<Notification, NotificationDto>().ReverseMap();
            CreateMap<Reservation, ReservationDto>().ReverseMap();
            
            CreateMap<Waitlist, WaitlistDto>().ReverseMap();
            CreateMap<BookSearchDto, Book>().ReverseMap();
            CreateMap<BookSearchDto, BookDto>().ReverseMap();
                
            
        }
    }
}
