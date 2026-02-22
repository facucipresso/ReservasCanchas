using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WinFormsApp1.Models.Reservation;
using WinFormsApp1.Models.Review;

namespace WinFormsApp1.UserControls
{
    public partial class UC_ReviewCard : UserControl
    {
        private int _reviewId;

        public event Action<int>? DeleteClicked;
        public UC_ReviewCard(ReviewResponseDTO dto)
        {
            InitializeComponent();

            _reviewId = dto.Id;

            labelNombreUser.Text = $"{dto.Name} {dto.LastName}";

            labelPuntaje.Text = new string('★', dto.Score) + new string('☆', 5 - dto.Score);

            labelComentario.Text = dto.Comment;

            buttonDeleteReview.Click += (s, e) =>
            {
                DeleteClicked?.Invoke(_reviewId);
            };
        }

    }
}
