using FluentValidation;
using Kutuphane.DAL.Dto.SubCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.Business.Validation.SubCategoryValidator
{
   public class UpdateSubCategoryValidator : AbstractValidator<UpdateSubCategoryDto>
    {
        public UpdateSubCategoryValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("İsim boş geçilemez.").MaximumLength(200).WithMessage("Maksımum 200 karakter giriniz.");
        }
    }
    
}
