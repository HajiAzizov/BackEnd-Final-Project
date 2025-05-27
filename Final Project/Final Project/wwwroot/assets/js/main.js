"use strict"

const swiper = new Swiper(".mySwiper", {
    loop: true,
    speed: 1000,
    navigation: {
      nextEl: ".swiper-button-next",
      prevEl: ".swiper-button-prev",
    },
    // Add other options like autoplay if needed
    // autoplay: {
    //   delay: 5000,
    //   disableOnInteraction: false,
    // },
  });



  document.addEventListener('DOMContentLoaded', function() {
    const addToCartButtons = document.querySelectorAll('.add-to-cart-btn');
    
    addToCartButtons.forEach(button => {
        button.addEventListener('click', function(e) {
            e.preventDefault();
            
            // Get book information
            const bookCard = this.closest('.book-card');
            const bookTitle = bookCard.querySelector('h3').textContent;
            const bookPrice = bookCard.querySelector('.price').textContent;
            
            // Show confirmation message (you can customize this)
            alert(`Added to cart: ${bookTitle} - ${bookPrice}`);
            
            // Here you would typically send data to a cart system
            console.log(`Book added to cart: ${bookTitle} - ${bookPrice}`);
        });
    });
});




document.addEventListener('DOMContentLoaded', function() {
    const shopNowBtn = document.querySelector('.bsb-cta-button');
    
    if (shopNowBtn) {
        shopNowBtn.addEventListener('click', function(e) {
            e.preventDefault();
            
            // Get book information
            const bookTitle = document.querySelector('.bsb-title-text').textContent;
            const bookPrice = document.querySelector('.bsb-price-text').textContent;
            const bookAuthor = document.querySelector('.bsb-author-text').textContent;
            
            // Show confirmation message (you can customize this)
            alert(`Redirecting to shop: ${bookTitle} - ${bookPrice} ${bookAuthor}`);
            
            // Here you would typically redirect to the product page or shopping cart
            console.log(`Shop Now clicked for: ${bookTitle} - ${bookPrice}`);
        });
    }
    
    // The hover effect is now handled in CSS for better performance
});