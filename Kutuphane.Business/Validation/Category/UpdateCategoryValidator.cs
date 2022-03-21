using FluentValidation;
using Kutuphane.DAL.Dto.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.Business.Validation.Category
{
  public class UpdateCategoryValidator : AbstractValidator<UpdateCategoryDto>
    {
        public UpdateCategoryValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("İsim boş geçilemez.")
                .MaximumLength(200).WithMessage("Maksımum 200 karakter giriniz.");
        }
    }

}

