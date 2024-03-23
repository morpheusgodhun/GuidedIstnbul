using Core.Entities;
using Core.IService;
using Core.StaticClass;
using Dto.ApiPanelDtos.ProductDtos.AddProductDtos;
using Dto.ApiPanelDtos.ProductDtos.GeneralDtos;
using DTO;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Text.Json;

namespace GuideAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class PanelProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IProductLanguageService _productLanguageService;
        private readonly IMany_Product_TagService _many_product_tagService;
        private readonly ITourService _tourService;
        private readonly ITourLanguageService _tourLanguageService;
        private readonly IServiceService _serviceService;
        private readonly IServiceLanguageService _serviceLanguageService;
        private readonly IMany_Tour_DestinationService _many_Tour_DestinationService;
        private readonly IMany_Tour_TourCategoryService _many_Tour_TourCategoryService;
        private readonly IMany_Tour_InclusionService _many_Tour_InclusionService;
        private readonly IMany_Tour_ExclusionService _many_Tour_ExclusionService;
        private readonly IMany_Tour_SightToSeeService _many_Tour_SightToSeeService;
        private readonly IMany_Product_AdditionalServiceService _many_Product_AdditionalServiceService;
        private readonly IMany_Product_AdditionalServiceOptionService _many_Product_AdditionalServiceOptionService;
        private readonly IProductImageService _productImageService;
        private readonly IMany_Product_RecomendedBlogService _many_Product_RecomendedBlogService;
        private readonly IBlogLanguageService _blogLanguageService;
        private readonly ITourProgramService _tourProgramService;
        private readonly ITourProgramLanguageService _tourProgramLanguageService;
        private readonly IAdditionalServiceLanguageService _additionalServiceLanguageService;
        private readonly IAdditionalServiceOptionLanguageService _additionalServiceOptionLanguageService;
        private readonly IAdditionalServiceOptionService _additionalServiceOptionService;
        private readonly IMany_Product_RoleTemplateService _many_product_roleTemplateService;

        public PanelProductController(IProductService productService, IProductLanguageService productLanguageService, IMany_Product_TagService many_product_tagService, ITourService tourService, ITourLanguageService tourLanguageService, IServiceService serviceService, IServiceLanguageService serviceLanguageService, IMany_Tour_DestinationService many_Tour_DestinationService, IMany_Tour_TourCategoryService many_Tour_TourCategoryService, IMany_Tour_InclusionService many_Tour_InclusionService, IMany_Tour_ExclusionService many_Tour_ExclusionService, IMany_Tour_SightToSeeService many_Tour_SightToSeeService, IMany_Product_AdditionalServiceService many_Product_AdditionalServiceService, IMany_Product_AdditionalServiceOptionService many_Product_AdditionalServiceOptionService, IProductImageService productImageService, IMany_Product_RecomendedBlogService many_Product_RecomendedBlogService, IBlogLanguageService blogLanguageService, ITourProgramService tourProgramService, ITourProgramLanguageService tourProgramLanguageService, IAdditionalServiceLanguageService additionalServiceLanguageService, IAdditionalServiceOptionLanguageService additionalServiceOptionLanguageService, IAdditionalServiceOptionService additionalServiceOptionService, IMany_Product_RoleTemplateService many_product_roleTemplateService)
        {
            _productService = productService;
            _productLanguageService = productLanguageService;
            _many_product_tagService = many_product_tagService;
            _tourService = tourService;
            _tourLanguageService = tourLanguageService;
            _serviceService = serviceService;
            _serviceLanguageService = serviceLanguageService;
            _many_Tour_DestinationService = many_Tour_DestinationService;
            _many_Tour_TourCategoryService = many_Tour_TourCategoryService;
            _many_Tour_InclusionService = many_Tour_InclusionService;
            _many_Tour_ExclusionService = many_Tour_ExclusionService;
            _many_Tour_SightToSeeService = many_Tour_SightToSeeService;
            _many_Product_AdditionalServiceService = many_Product_AdditionalServiceService;
            _many_Product_AdditionalServiceOptionService = many_Product_AdditionalServiceOptionService;
            _productImageService = productImageService;
            _many_Product_RecomendedBlogService = many_Product_RecomendedBlogService;
            _blogLanguageService = blogLanguageService;
            _tourProgramService = tourProgramService;
            _tourProgramLanguageService = tourProgramLanguageService;
            _additionalServiceLanguageService = additionalServiceLanguageService;
            _additionalServiceOptionLanguageService = additionalServiceOptionLanguageService;
            _additionalServiceOptionService = additionalServiceOptionService;
            _many_product_roleTemplateService = many_product_roleTemplateService;
        }

        //Product'ı ekledikten sonra eklenen Product'ın ID'sini döner.
        [HttpPost]
        public async Task<CustomResponseDto<Guid>> AddPorduct(AddProductDto addProductDto)
        {
            Product product = new()
            {
                ProductName = addProductDto.ProductName,
                YoutubeLink = addProductDto.YoutubeLink,
                BannerImagePath = addProductDto.BannerImagePath,
                CardImagePath = addProductDto.CardImagePath,
                CancellationPolicyID = addProductDto.CancellationPolicyID,
                CutoffHour = addProductDto.CutOffDay * 24 + addProductDto.CutOffHour,
                CustomerDeposito = addProductDto.CustomerDepositoAmount,
                AgentDeposito = addProductDto.AgentDepositoAmount,
                PaymentDate = addProductDto.DayOfPayment,
                ShowOnHomePage = addProductDto.ShowOnHomepage,
                MinimumPayoutPercent = addProductDto.MinimumPayoutPercent,
                IsChildPolicyActive = addProductDto.ActivateChildPolicy,
                IsTour = addProductDto.IsProductTour,
                Order = addProductDto.Order,
                Status = false
            };

            await _productService.AddAsync(product);

            foreach (var language in LanguageList.AllLanguages)
            {
                ProductLanguageItem productLanguageItem = new()
                {
                    LanguageID = language.Id,
                    ProductID = product.Id,
                    DisplayName = addProductDto.DisplayName,
                    Slug = addProductDto.Slug,
                    Product = product
                };
                await _productLanguageService.AddAsync(productLanguageItem);
            }

            foreach (var tagID in addProductDto.TagIDs)
            {
                Many_Product_Tag many_tag = new()
                {
                    TagID = tagID,
                    ProductID = product.Id,
                };
                await _many_product_tagService.AddAsync(many_tag);
            }

            if (addProductDto.IsProductTour)
            {
                Tour tour = new()
                {
                    ProductID = product.Id,
                    CustomTourRequestId = addProductDto.RequestId == Guid.Empty ? null : addProductDto.RequestId
                };
                await _tourService.AddAsync(tour);

                product.TourID = tour.Id;
                _productService.Update(product);

                foreach (var language in LanguageList.AllLanguages)
                {
                    TourLanguageItem tourLanguageItem = new TourLanguageItem()
                    {
                        LanguageID = language.Id,
                        TourID = tour.Id,
                    };
                    await _tourLanguageService.AddAsync(tourLanguageItem);
                }
            }
            else
            {
                Core.Entities.Service service = new Core.Entities.Service()
                {
                    ProductID = product.Id,

                };
                await _serviceService.AddAsync(service);

                product.ServiceID = service.Id;
                _productService.Update(product);

                foreach (var language in LanguageList.AllLanguages)
                {
                    ServiceLanguageItem serviceLanguageItem = new()
                    {
                        LanguageID = language.Id,
                        ServiceID = service.Id,
                    };
                    await _serviceLanguageService.AddAsync(serviceLanguageItem);
                }
            }
            return CustomResponseDto<Guid>.Success(204, product.Id);
        }

        [HttpPost]
        public async Task<CustomResponseNullDto> AddTour(AddTourDto addTourDto)
        {
            Product product = await _productService.GetByIdAsync(addTourDto.ProductID);
            product.IsPersonLimited = addTourDto.IsPersonLimited;
            product.PersonLimit = addTourDto.PersonLimit;
            _productService.Update(product);

            Tour tour = _tourService.Where(x => x.ProductID == addTourDto.ProductID).FirstOrDefault();
            tour.TourTypeID = addTourDto.TourTypeID;
            tour.SegmentID = addTourDto.SegmentID;
            tour.StartCityID = addTourDto.StartCityID;
            tour.StartTimeIDs = JsonSerializer.Serialize(addTourDto.StartTimeIDs);

            if (addTourDto.SuggestedStartTimeID == 0)
                addTourDto.SuggestedStartTimeID = 1;

            tour.SuggestedStartTimeID = addTourDto.SuggestedStartTimeID;

            var selectableDurations = $"[{string.Join(",", addTourDto.SelectableDurations.Select(x => x.ToString()))}]";
            tour.SelectableDurations = selectableDurations;
            tour.Duration = addTourDto.SelectableDurations.Max();

            //tour.IsPerPerson = addTourDto.IsPerPerson;
            //tour.IsPerDay = selectableDurations.Contains(",") ? true : addTourDto.IsPerDay; // eğer birden fazla gün seçme olursa burası mutlaka perday olmalı
            _tourService.Update(tour);

            //for (int i = 1; i <= addTourDto.Duration; i++)
            for (int i = 1; i <= addTourDto.SelectableDurations.Max(); i++)
            {
                TourProgram program = new TourProgram()
                {
                    Day = i,
                    TourID = tour.Id,
                };
                await _tourProgramService.AddAsync(program);

                foreach (var language in LanguageList.AllLanguages)
                {
                    TourProgramLanguageItem tourProgramLanguageItem = new TourProgramLanguageItem()
                    {
                        LanguageID = language.Id,
                        TourProgramID = program.Id,
                        Title = "",
                        Content = ""
                    };
                    await _tourProgramLanguageService.AddAsync(tourProgramLanguageItem);
                }
            }

            foreach (var language in LanguageList.AllLanguages)
            {
                TourLanguageItem tourLanguageItem = _tourLanguageService.Where(x => x.TourID == tour.Id && x.LanguageID == language.Id).FirstOrDefault();

                tourLanguageItem.DurationText = addTourDto.DurationText;
                tourLanguageItem.TourStartPoint = addTourDto.StartPoint;
                tourLanguageItem.TourEndPoint = addTourDto.EndPoint;
                tourLanguageItem.Content = addTourDto.Content;
                _tourLanguageService.Update(tourLanguageItem);
            }
            if (addTourDto.AdditionalServices is not null && addTourDto.AdditionalServices.Count > 0)
            {
                foreach (var additionalServiceDto in addTourDto.AdditionalServices)
                {
                    Many_Product_AdditionalService manyAdditional = new Many_Product_AdditionalService()
                    {
                        ProductID = addTourDto.ProductID,
                        AdditionalServiceID = additionalServiceDto.AdditionalServiceID,
                        IsRequired = additionalServiceDto.IsRequired,
                        IsMultiSelect = additionalServiceDto.IsMultiSelect,
                        Order = additionalServiceDto.Order,
                    };

                    await _many_Product_AdditionalServiceService.AddAsync(manyAdditional);
                    if (additionalServiceDto.OptionIDs is not null)
                    {
                        foreach (var optionID in additionalServiceDto.OptionIDs)
                        {
                            Many_Product_AdditionalServiceOption manyOption = new Many_Product_AdditionalServiceOption()
                            {
                                AdditionalServiceOptionID = optionID,
                                Many_Product_AdditionalServiceID = manyAdditional.Id
                            };
                            await _many_Product_AdditionalServiceOptionService.AddAsync(manyOption);

                        }
                    }
                }
            }

            foreach (var destinationID in addTourDto.DestinationIDs)
            {
                Many_Tour_Destination many = new Many_Tour_Destination()
                {
                    TourID = tour.Id,
                    DestinationID = destinationID
                };
                await _many_Tour_DestinationService.AddAsync(many);
            }

            foreach (var tourCategoryID in addTourDto.TourCategoryIDs)
            {
                Many_Tour_TourCategory many = new Many_Tour_TourCategory()
                {
                    TourID = tour.Id,
                    TourCategoryID = tourCategoryID
                };
                await _many_Tour_TourCategoryService.AddAsync(many);
            }

            foreach (var inclusionID in addTourDto.InclusionsIDs)
            {
                Many_Tour_Inclusion many = new Many_Tour_Inclusion()
                {
                    TourID = tour.Id,
                    SystemOptionID = inclusionID
                };
                await _many_Tour_InclusionService.AddAsync(many);
            }

            foreach (var exclusionID in addTourDto.ExclusionsIDs)
            {
                Many_Tour_Exclusion many = new Many_Tour_Exclusion()
                {
                    TourID = tour.Id,
                    SystemOptionID = exclusionID
                };
                await _many_Tour_ExclusionService.AddAsync(many);
            }

            foreach (var sightToSeeID in addTourDto.SightsToSeeIDs)
            {
                Many_Tour_SightToSee many = new Many_Tour_SightToSee()
                {
                    TourID = tour.Id,
                    SystemOptionID = sightToSeeID
                };
                await _many_Tour_SightToSeeService.AddAsync(many);
            }



            return CustomResponseNullDto.Success(204);
        }

        [HttpPost]
        public CustomResponseNullDto AddService(AddServiceDto addServiceDto)
        {

            var service = _serviceService.Where(x => x.ProductID == addServiceDto.ProductID).FirstOrDefault();

            foreach (var language in LanguageList.AllLanguages)
            {
                ServiceLanguageItem serviceLanguageItem = _serviceLanguageService.Where(x => x.ServiceID == service.Id && x.LanguageID == language.Id).FirstOrDefault();

                serviceLanguageItem.ShortDescription = addServiceDto.ShortDescription;
                serviceLanguageItem.Content = addServiceDto.Content;
                _serviceLanguageService.Update(serviceLanguageItem);
            }

            foreach (var additionalServiceDto in addServiceDto.AdditionalServices)
            {
                Many_Product_AdditionalService manyAdditional = new Many_Product_AdditionalService()
                {
                    ProductID = addServiceDto.ProductID,
                    AdditionalServiceID = additionalServiceDto.AdditionalServiceID,
                    IsRequired = additionalServiceDto.IsRequired,
                    IsMultiSelect = additionalServiceDto.IsMultiSelect,
                    Order = additionalServiceDto.Order,
                };

                _many_Product_AdditionalServiceService.Add(manyAdditional);

                foreach (var optionID in additionalServiceDto.OptionIDs)
                {
                    Many_Product_AdditionalServiceOption manyOption = new Many_Product_AdditionalServiceOption()
                    {
                        AdditionalServiceOptionID = optionID,
                        Many_Product_AdditionalServiceID = manyAdditional.Id
                    };
                    _many_Product_AdditionalServiceOptionService.Add(manyOption);

                }
            }
            return CustomResponseNullDto.Success(204);
        }


        [HttpGet("{id}")]
        public CustomResponseNullDto ToggleProductStatus(Guid id)
        {

            _productService.ToggleStatus(id);


            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseNullDto ToggleShowOnHomepage(Guid id)
        {

            Product product = _productService.GetById(id);
            product.ShowOnHomePage = !product.ShowOnHomePage;
            _productService.Update(product);

            return CustomResponseNullDto.Success(204);
        }

        //Blog

        [HttpGet("{id}")]
        public CustomResponseDto<List<ProductBlogListDto>> ProductBlogList(Guid id)
        {
            List<ProductBlogListDto> productBlogListDtos = (from many in _many_Product_RecomendedBlogService.GetAll(x => x.ProductID == id)
                                                            join blogLanguageItem in _blogLanguageService.GetAll()
                                                            on many.BlogID equals blogLanguageItem.BlogID
                                                            where blogLanguageItem.LanguageID == LanguageList.BaseLanguage.Id
                                                            select new ProductBlogListDto()
                                                            {
                                                                BlogID = many.BlogID,
                                                                ProductBlogID = many.Id,
                                                                Title = blogLanguageItem.BlogTitle,
                                                                ShortDescription = blogLanguageItem.ShortDescription
                                                            }).ToList();

            return CustomResponseDto<List<ProductBlogListDto>>.Success(200, productBlogListDtos);
        }

        [HttpPost]
        public CustomResponseNullDto AddProductBlog(AddProductBlogDto addProductBlogDto)
        {

            Many_Product_RecomendedBlog many = new Many_Product_RecomendedBlog()
            {
                ProductID = addProductBlogDto.ProductID,
                BlogID = addProductBlogDto.BlogID
            };

            _many_Product_RecomendedBlogService.Add(many);


            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseNullDto DeleteProductBlog(Guid id)
        {
            Many_Product_RecomendedBlog many = _many_Product_RecomendedBlogService.GetById(id);

            _many_Product_RecomendedBlogService.Remove(many);

            return CustomResponseNullDto.Success(204);
        }

        //Blog End
        //ProductImage

        [HttpGet("{id}")]
        public CustomResponseDto<List<ProductImageListDto>> ProductImageList(Guid id)
        {
            List<ProductImageListDto> productImageListDtos = _productImageService.GetProductImageList(id).ProductImages;
            return CustomResponseDto<List<ProductImageListDto>>.Success(200, productImageListDtos);
        }
        [HttpGet("{id}")]
        public CustomResponseDto<TourDetailProductImageListDto> TourDetailProductImageList(Guid id)
        {
            TourDetailProductImageListDto productImageListDtos = _productImageService.GetProductImageList(id);
            return CustomResponseDto<TourDetailProductImageListDto>.Success(200, productImageListDtos);
        }


        [HttpPost]
        public CustomResponseNullDto AddProductImage(AddProductImageDto addProductImageDto)
        {

            ProductImage productImage = new ProductImage()
            {
                ProductID = addProductImageDto.ProductID,
                Order = addProductImageDto.Order,
                ImagePath = addProductImageDto.ImagePath,
            };
            _productImageService.Add(productImage);


            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseDto<EditProductImageDto> EditProductImage(Guid id)
        {

            ProductImage productImage = _productImageService.GetById(id);

            EditProductImageDto editProductImageDto = new EditProductImageDto()
            {
                ProductImageID = productImage.Id,
                ProductID = productImage.ProductID,
                ImagePath = productImage.ImagePath,
                Order = productImage.Order
            };

            return CustomResponseDto<EditProductImageDto>.Success(200, editProductImageDto);
        }

        [HttpPost]
        public CustomResponseNullDto EditProductImage(EditProductImageDto editProductImageDto)
        {

            ProductImage productImage = _productImageService.GetById(editProductImageDto.ProductImageID);

            productImage.ImagePath = editProductImageDto.ImagePath;
            productImage.Order = editProductImageDto.Order;

            _productImageService.Update(productImage);

            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseNullDto ToggleProductImageStatus(Guid id)
        {
            _productImageService.ToggleStatus(id);


            return CustomResponseNullDto.Success(204);
        }

        //ProductImage End

        //Done


        [HttpPost]
        public CustomResponseNullDto CloneProduct(CloneProductDto cloneProductDto)
        {

            return CustomResponseNullDto.Success(204);
        }

        [HttpPost]
        public async Task<CustomResponseNullDto> PostProduct(PostProductDto postProductDto)
        {
            var productManyRoleTemplates = (await _many_product_roleTemplateService.GetWhereAsync(x => x.ProductId == new Guid(postProductDto.ProductID))).ToList();
            productManyRoleTemplates.ForEach(x =>
            {
                _many_product_roleTemplateService.Remove(x);
            });
            foreach (Guid id in postProductDto.RoleTemplateIds)
                await _many_product_roleTemplateService.AddAsync(new(new Guid(postProductDto.ProductID), id));

            return CustomResponseNullDto.Success(204);
        }

        //Additional Service
        [HttpGet("{id}")]
        public async Task<CustomResponseDto<List<ProductAdditionalServiceListDto>>> AdditionalServiceList(Guid id)
        {
            var additionalServiceList = await _many_Product_AdditionalServiceOptionService.GetAllAsync();
            List<ProductAdditionalServiceListDto> additionalServiceListDtos = (from many in _many_Product_AdditionalServiceService.GetAll()
                                                                               where many.ProductID == id
                                                                               join additionalServiceLanguage in _additionalServiceLanguageService.GetAll()
                                                                               on many.AdditionalServiceID equals additionalServiceLanguage.AdditionalServiceID
                                                                               where additionalServiceLanguage.LanguageID == LanguageList.BaseLanguage.Id
                                                                               select new ProductAdditionalServiceListDto()
                                                                               {
                                                                                   ProductAdditionalServiceID = many.Id,
                                                                                   IsRequired = many.IsRequired,
                                                                                   Status = many.Status,
                                                                                   IsMultiSelect = many.IsMultiSelect,
                                                                                   IsComissionValid = many.ComissionRateAbility,
                                                                                   Order = many.Order,
                                                                                   Title = additionalServiceLanguage.DisplayName,
                                                                                   Options = (from many2 in _many_Product_AdditionalServiceOptionService.GetAll()
                                                                                              where many.Id == many2.Many_Product_AdditionalServiceID
                                                                                              join optionLanguage in _additionalServiceOptionLanguageService.GetAll()
                                                                                              on many2.AdditionalServiceOptionID equals optionLanguage.AdditionalServiceOptionID
                                                                                              where optionLanguage.LanguageID == LanguageList.BaseLanguage.Id
                                                                                              select optionLanguage.DisplayName).ToList(),
                                                                                   AdditionalServiceID = many.AdditionalServiceID,
                                                                                   OptionIDs = (from many2 in _many_Product_AdditionalServiceOptionService.GetAll()
                                                                                                where many.Id == many2.Many_Product_AdditionalServiceID
                                                                                                join option in _additionalServiceOptionService.GetAll()
                                                                                                on many2.AdditionalServiceOptionID equals option.Id
                                                                                                select option.Id).ToList(),
                                                                               }).ToList();



            return CustomResponseDto<List<ProductAdditionalServiceListDto>>.Success(200, additionalServiceListDtos);
        }

        [HttpPost]
        public CustomResponseNullDto AddProductAdditionalService(AddProductAdditionalServiceDto addProductAdditionalService)
        {
            Many_Product_AdditionalService many_Product_AdditionalService = new()
            {
                ProductID = addProductAdditionalService.ProductID,
                AdditionalServiceID = addProductAdditionalService.AdditionalServiceID,
                IsRequired = addProductAdditionalService.IsRequired,
                IsMultiSelect = addProductAdditionalService.IsMultiSelect,
                ComissionRateAbility = addProductAdditionalService.IsComissionValid,
                Order = addProductAdditionalService.Order,
            };
            _many_Product_AdditionalServiceService.Add(many_Product_AdditionalService);


            foreach (var optionID in addProductAdditionalService.OptionIDs)
            {
                Many_Product_AdditionalServiceOption many = new Many_Product_AdditionalServiceOption()
                {
                    AdditionalServiceOptionID = optionID,
                    Many_Product_AdditionalServiceID = many_Product_AdditionalService.Id,
                };
                _many_Product_AdditionalServiceOptionService.Add(many);
            }

            return CustomResponseNullDto.Success(204);
        }

        [HttpGet("{id}")]
        public CustomResponseDto<EditProductAdditionalServiceDto> EditProductAdditionalService(Guid id)
        {

            EditProductAdditionalServiceDto editProductAdditionalServiceDto = (from many in _many_Product_AdditionalServiceService.GetAll()
                                                                               where many.Id == id
                                                                               select new EditProductAdditionalServiceDto()
                                                                               {
                                                                                   ProductAdditionalServiceID = many.Id,
                                                                                   AdditionalServiceID = many.AdditionalServiceID,
                                                                                   AdditionalServiceName = _additionalServiceLanguageService.GetAll(x => x.AdditionalServiceID == many.AdditionalServiceID && x.LanguageID == LanguageList.BaseLanguage.Id).FirstOrDefault().DisplayName,
                                                                                   Order = many.Order,
                                                                                   IsMultiSelect = many.IsMultiSelect,
                                                                                   IsComissionValid = many.ComissionRateAbility,
                                                                                   IsRequired = many.IsRequired,
                                                                                   OptionIDs = (from many2 in _many_Product_AdditionalServiceOptionService.GetAll()
                                                                                                where many2.Many_Product_AdditionalServiceID == many.Id
                                                                                                select many2.AdditionalServiceOptionID).ToList()
                                                                               }).FirstOrDefault();

            return CustomResponseDto<EditProductAdditionalServiceDto>.Success(200, editProductAdditionalServiceDto);
        }

        [HttpPost]
        public CustomResponseDto<Guid> EditProductAdditionalService(EditProductAdditionalServiceDto editProductAdditionalServiceDto)
        {

            Many_Product_AdditionalService many_Product_AdditionalService = _many_Product_AdditionalServiceService.GetById(editProductAdditionalServiceDto.ProductAdditionalServiceID);

            many_Product_AdditionalService.AdditionalServiceID = editProductAdditionalServiceDto.AdditionalServiceID;
            many_Product_AdditionalService.IsRequired = editProductAdditionalServiceDto.IsRequired;
            many_Product_AdditionalService.ComissionRateAbility = editProductAdditionalServiceDto.IsComissionValid;
            many_Product_AdditionalService.IsMultiSelect = editProductAdditionalServiceDto.IsMultiSelect;
            many_Product_AdditionalService.Order = editProductAdditionalServiceDto.Order;

            _many_Product_AdditionalServiceService.Update(many_Product_AdditionalService);


            var oldOptions = _many_Product_AdditionalServiceOptionService.GetAll(x => x.Many_Product_AdditionalServiceID == many_Product_AdditionalService.Id);

            foreach (var option in oldOptions)
            {
                _many_Product_AdditionalServiceOptionService.Remove(option);
            }

            foreach (var optionID in editProductAdditionalServiceDto.OptionIDs)
            {
                Many_Product_AdditionalServiceOption many = new Many_Product_AdditionalServiceOption()
                {
                    AdditionalServiceOptionID = optionID,
                    Many_Product_AdditionalServiceID = many_Product_AdditionalService.Id,
                };
                _many_Product_AdditionalServiceOptionService.Add(many);
            }

            return CustomResponseDto<Guid>.Success(204, many_Product_AdditionalService.ProductID);
        }

        [HttpGet("{id}")]
        public CustomResponseNullDto ToggleProductAdditionalServiceStatus(Guid id)
        {

            _many_Product_AdditionalServiceService.ToggleStatus(id);

            return CustomResponseNullDto.Success(204);

        }

        //Additional Service End

        [HttpGet("{id}")]
        public CustomResponseDto<string> GetProductName(Guid id)
        {
            var productName = _productService.GetById(id).ProductName;
            return CustomResponseDto<string>.Success(200, productName);
        }

    }
}
