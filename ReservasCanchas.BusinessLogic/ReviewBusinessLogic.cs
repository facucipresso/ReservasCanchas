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
        private readonly UserBusinessLogic _userBusinessLogic;
        private readonly ReservationBusinessLogic _reservationBusinessLogic;
        private readonly ComplexBusinessLogic _complexBusinessLogic;
        private readonly AuthService _authService;
        public ReviewBusinessLogic(ReviewRepository reviewRepository, UserBusinessLogic usuarioBusinessLogic,
            ReservationBusinessLogic reservationBusinessLogic, ComplexBusinessLogic complexBusinessLogic, AuthService authService)
        {
            _reviewRepository = reviewRepository;
            _userBusinessLogic = usuarioBusinessLogic;
            _reservationBusinessLogic = reservationBusinessLogic;
            _complexBusinessLogic = complexBusinessLogic;
            _authService = authService;
        }

        public async Task<ReviewResponseDTO> GetReviewByIdAsync(int id)
        {
            var review = await _reviewRepository.GetReviewByIdAsync(id);
            if (review == null)
                throw new NotFoundException($"Review con id {id} no encontrada");

            return ReviewMapper.ToReviewResponseDTO(review);
        }

        public async Task<ReviewResponseDTO> GetReviewByReservationIdAsync(int reservationId)
        {
            var reservation = await _reservationBusinessLogic.GetReservationWithRelationsOrThrow(reservationId);

            var review = await _reviewRepository.GetReviewByReservationIdAsync(reservationId);

            if (review == null)
                throw new NotFoundException($"Review no encontrada para la reserva con id {reservationId}");

            return ReviewMapper.ToReviewResponseDTO(review);
        }

        public async Task<List<ReviewResponseDTO>> GetReviewsByComplexIdAsync(int complexId)
        {
            await _complexBusinessLogic.GetComplexBasicOrThrow(complexId);

            var reviews = await _reviewRepository.GetReviewsByComplexIdAsync(complexId);

            return reviews.Select(ReviewMapper.ToReviewResponseDTO).ToList();
        }

        public async Task<List<ReviewResponseDTO>> GetReviewsByUserAsync()
        {
            var userId =_authService.GetUserId();

            var user = await _userBusinessLogic.GetUserOrThrow(userId);
            await _userBusinessLogic.ValidateUserState(user);

            var reviews = await _reviewRepository.GetReviewsByUserIdAsync(userId);

            return reviews.Select(ReviewMapper.ToReviewResponseDTO).ToList();
        }

        public async Task<ReviewResponseDTO> CreateReviewAsync(CreateReviewRequestDTO createReviewDTO)
        {
            var userRol = _authService.GetUserRole();
            var userId = _authService.GetUserId();

            var user = await _userBusinessLogic.GetUserOrThrow(userId);
            await _userBusinessLogic.ValidateUserState(user);

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

            return ReviewMapper.ToReviewResponseDTO(review);
        }

        public async Task DeleteReview(int id)
        {
            var userId = _authService.GetUserId(); //_authService.GetUserIdFromToken();

            var review = await _reviewRepository.GetReviewByIdAsync(id);

            if (review == null)
                throw new NotFoundException($"Review con id {id} no encontrada");

            if (userId != review.UserId)
                throw new BadRequestException("La review solo puede ser eliminada por el usuario que la realizó");

            await _reviewRepository.DeleteReviewAsync(review);
        }
    }
}
