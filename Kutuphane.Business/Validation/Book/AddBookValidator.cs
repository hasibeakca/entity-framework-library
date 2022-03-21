using FluentValidation;
using Kutuphane.DAL.Dto.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kutuphane.Business.Validation.Book
{
   public  class AddBookValidator : AbstractValidator<AddBookDto>
    {
        public AddBookValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Kıtap adı boş geçilemez.").MaximumLength(200)
                .WithMessage("En fazla 200 karakter giriniz.");
            RuleFor(p => p.Desc).NotEmpty().WithMessage("Description boş geçilemez.")
                .MaximumLength(200).WithMessage("Description maksımum 200 karakter olmalıdır.");
        }
    }
}
