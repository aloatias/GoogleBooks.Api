using AutoMapper;
using GoogleBooks.Api.Dtos.Output;
using GoogleBooks.Client.Dtos.Output;

namespace GoogleBooks.Api.Configuration.Mapping
{
    public class BooksCatalogProfile : Profile
    {
        public BooksCatalogProfile()
        {
            CreateMap<Client.Dtos.Output.ReadingModes, Dtos.Output.ReadingModes>();

            CreateMap<GoogleBookDetailsLite, BookDetailsForCatalog>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.AccessViewStatus, opt => opt.MapFrom(src => src.AccessInfo.AccessViewStatus))
                .ForMember(dest => dest.AllowAnonLogging, opt => opt.MapFrom(src => src.VolumeInfo.AllowAnonLogging))
                .ForMember(dest => dest.Authors, opt => opt.MapFrom(src => src.VolumeInfo.Authors))
                .ForMember(dest => dest.CanonicalVolumeLink, opt => opt.MapFrom(src => src.VolumeInfo.CanonicalVolumeLink))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.VolumeInfo.Categories))
                .ForMember(dest => dest.ContentVersion, opt => opt.MapFrom(src => src.VolumeInfo.ContentVersion))
                .ForMember(dest => dest.Country, opt => opt.MapFrom(src => src.AccessInfo.Country))
                .ForMember(dest => dest.Description, opt => opt.MapFrom(src => src.VolumeInfo.Description))
                .ForMember(dest => dest.Embeddable, opt => opt.MapFrom(src => src.AccessInfo.Embeddable))
                .ForMember(dest => dest.Etag, opt => opt.MapFrom(src => src.Etag))
                .ForMember(dest => dest.InfoLink, opt => opt.MapFrom(src => src.VolumeInfo.InfoLink))
                .ForMember(dest => dest.IsEbook, opt => opt.MapFrom(src => src.SaleInfo.IsEbook))
                .ForMember(dest => dest.IsPdfAvailable, opt => opt.MapFrom(src => src.AccessInfo.Pdf.IsAvailable))
                .ForMember(dest => dest.Kind, opt => opt.MapFrom(src => src.Kind))
                .ForMember(dest => dest.Language, opt => opt.MapFrom(src => src.VolumeInfo.Language))
                .ForMember(dest => dest.MaturityRating, opt => opt.MapFrom(src => src.VolumeInfo.MaturityRating))
                .ForMember(dest => dest.PageCount, opt => opt.MapFrom(src => src.VolumeInfo.PageCount))
                .ForMember(dest => dest.PdfActsTokenLink, opt => opt.MapFrom(src => src.AccessInfo.Pdf.ActsTokenLink))
                .ForMember(dest => dest.PreviewLink, opt => opt.MapFrom(src => src.VolumeInfo.PreviewLink))
                .ForMember(dest => dest.PrintType, opt => opt.MapFrom(src => src.VolumeInfo.PrintType))
                .ForMember(dest => dest.PrintedPageCount, opt => opt.MapFrom(src => src.VolumeInfo.PrintedPageCount))
                .ForMember(dest => dest.PublicDomain, opt => opt.MapFrom(src => src.AccessInfo.PublicDomain))
                .ForMember(dest => dest.PublishedDate, opt => opt.MapFrom(src => src.VolumeInfo.PublishedDate))
                .ForMember(dest => dest.Publisher, opt => opt.MapFrom(src => src.VolumeInfo.Publisher))
                .ForMember(dest => dest.QuoteSharingAllowed, opt => opt.MapFrom(src => src.AccessInfo.QuoteSharingAllowed))
                .ForMember(dest => dest.ReadingModes, opt => opt.MapFrom(src => src.VolumeInfo.ReadingModes))
                .ForMember(dest => dest.Saleability, opt => opt.MapFrom(src => src.SaleInfo.Saleability))
                .ForMember(dest => dest.SelfLink, opt => opt.MapFrom(src => src.SelfLink))
                .ForMember(dest => dest.SmallThumbNail, opt => opt.MapFrom(src => src.VolumeInfo.ImageLinks.SmallThumbnail))
                .ForMember(dest => dest.TextToSpeechPermission, opt => opt.MapFrom(src => src.AccessInfo.TextToSpeechPermission))
                .ForMember(dest => dest.Thumbnail, opt => opt.MapFrom(src => src.VolumeInfo.ImageLinks.Thumbnail))
                .ForMember(dest => dest.Title, opt => opt.MapFrom(src => src.VolumeInfo.Title))
                .ForMember(dest => dest.WebReaderLink, opt => opt.MapFrom(src => src.AccessInfo.WebReaderLink));
        }
    }
}
