using Microsoft.AspNetCore.Mvc;
using ReservasCanchas.BusinessLogic.Dtos.Review;
using ReservasCanchas.BusinessLogic.Exceptions;
using ReservasCanchas.BusinessLogic.Mappers;
using ReservasCanchas.DataAccess.Repositories;
using ReservasCanchas.Domain.Entities;
using ReservasCanchas.Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReservasCanchas.BusinessLogic
{
    public class ReviewBusinessLogic
    {
        private readonly ReviewRepository _reviewRepository;
        private readonly UsuarioBusinessLogic _usurioBusinessLogic;
        private readonly ReservationBusinessLogic _reservationBusinessLogic;
        public ReviewBusinessLogic(ReviewRepository reviewRepository, UsuarioBusinessLogic usuarioBusinessLogic, ReservationBusinessLogic reservationBusinessLogic)
        {
            _reviewRepository = reviewRepository;
            _usurioBusinessLogic = usuarioBusinessLogic;
            _reservationBusinessLogic = reservationBusinessLogic;
        }

        public async Task<ReviewResponseDTO> GetReviewByIdAsync(int id)
        {
            Review review = await _reviewRepository.GetReviewByIdAsync(id);
            if (review == null)
                throw new NotFoundException($"Review con id {id} no encontrada");

            return ReviewMapper.toReviewResponseDTO(review);
        }
        public async Task<CreateReviewResponseDTO> CreateReviewAsync(CreateReviewRequestDTO createReviewDTO)
        {
            var userRol = Rol.AdminComplejo; //_authService.GetUserRolFromToken();
            var userId = 1; //_authService.GetUserIdFromToken();

            // Esto me da un usuario Dto, que si no existe o si no esta habilitado me tira solo el error
            var user = await _usurioBusinessLogic.GetUserOrThrow(userId);

            // si no existe me patea
            var reservation = await _reservationBusinessLogic.GetReservationWithRelationsOrThrow(createReviewDTO.ReservationId);

            if(reservation.UserId != userId)
                throw new BadRequestException("Solo el usuario que realizó la reserva puede dejar una review.");

            if (reservation.ReservationState != ReservationState.Completada)
                throw new BadRequestException("Solo se puede dejar una reseña si la reserva fue completada.");

            if (reservation.Review != null)
                throw new BadRequestException("Esta reserva ya tiene una review.");

            Review review = ReviewMapper.ToReview(createReviewDTO, userId);
            review.CreationDate = DateTime.Now;

            await _reviewRepository.CreateReviewAsync(review);

            return ReviewMapper.ToCreateReviewResponseDTO(review);

        }
    }
}
