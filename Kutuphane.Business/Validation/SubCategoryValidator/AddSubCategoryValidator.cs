using FluentValidation;
using Kutuphane.DAL.Dto.SubCategory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.Business.Validation.SubCategoryValidator
{
   public class AddSubCategoryValidator : AbstractValidator<AddSubCategoryDto>
    {
        public AddSubCategoryValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("İsim boş geçilemez.").MaximumLength(200).WithMessage("Maksımum 200 karakter giriniz.");
        }
    }
}
