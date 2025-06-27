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

const userLat = 40.3779;
const userLng = 49.8919;

function initMap() {
  const userLocation = { lat: userLat, lng: userLng };
  const map = new google.maps.Map(document.getElementById("map"), {
    zoom: 10,
    center: userLocation,
  });

  new google.maps.Marker({
    position: userLocation,
    map: map,
    title: "User Location",
  });
}


document.querySelectorAll('.toggle-password').forEach(toggle => {
  toggle.addEventListener('click', () => {
    const targetId = toggle.getAttribute('data-target');
    const input = document.getElementById(targetId);
    const icon = toggle.querySelector('i');
    if (input.type === 'password') {
      input.type = 'text';
      icon.classList.remove('fa-eye-slash');
      icon.classList.add('fa-eye');
    } else {
      input.type = 'password';
      icon.classList.remove('fa-eye');
      icon.classList.add('fa-eye-slash');
    }
  });
});

  const rangeMin = document.getElementById('rangeMin');
  const rangeMax = document.getElementById('rangeMax');
  const rangeMinVal = document.getElementById('rangeMinVal');
  const rangeMaxVal = document.getElementById('rangeMaxVal');

  function updateRange() {
    let minVal = parseInt(rangeMin.value);
    let maxVal = parseInt(rangeMax.value);

    if (minVal > maxVal - 1) {
      minVal = maxVal - 1;
      rangeMin.value = minVal;
    }
    if (maxVal < minVal + 1) {
      maxVal = minVal + 1;
      rangeMax.value = maxVal;
    }

    rangeMinVal.textContent = `$${minVal}`;
    rangeMaxVal.textContent = `$${maxVal}`;
  }

  rangeMin.addEventListener('input', updateRange);
  rangeMax.addEventListener('input', updateRange);

  // Initialize values
updateRange();



