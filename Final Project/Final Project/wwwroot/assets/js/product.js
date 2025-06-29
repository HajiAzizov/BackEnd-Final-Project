"use strict";

$(function () {
    // Show More düyməsi - gizli məhsulları göstərir
    $('#showMoreBtn').on('click', function () {
        $('.more-product').removeClass('d-none').show();
        $(this).hide();
    });

    function applyFilters() {
        const searchText = $('#liveSearchInput').val().toLowerCase().trim();

        const selectedGenres = $('.filter-genre:checked').map(function () {
            return $(this).val();
        }).get();

        const selectedAuthors = $('.filter-author:checked').map(function () {
            return $(this).val();
        }).get();

        const minPrice = parseFloat($('#priceMin').val()) || 0;
        const maxPrice = parseFloat($('#priceMax').val()) || 1000000;

        let shownCount = 0;
        const showMoreVisible = $('#showMoreBtn').is(':visible');

        $('#productsContainer > div.col-lg-4').each(function () {
            const $prod = $(this);
            const name = $prod.find('h2').text().toLowerCase();
            const authorsText = $prod.find('h4').text().toLowerCase();
            const prodGenres = ($prod.data('genres') || '').toString().split(',');
            const prodAuthors = ($prod.data('authors') || '').toString().split(',');
            const price = parseFloat($prod.data('price')) || 0;

            const matchesSearch = !searchText || name.includes(searchText) || authorsText.includes(searchText);
            const matchesGenre = selectedGenres.length === 0 || selectedGenres.some(g => prodGenres.includes(g));
            const matchesAuthor = selectedAuthors.length === 0 || selectedAuthors.some(a => prodAuthors.includes(a));
            const matchesPrice = price >= minPrice && price <= maxPrice;

            if (matchesSearch && matchesGenre && matchesAuthor && matchesPrice) {
                if (showMoreVisible) {
                    // Show only first 3 when Show More visible
                    if (shownCount < 3) {
                        $prod.show().removeClass('d-none');
                        shownCount++;
                    } else {
                        $prod.hide().addClass('d-none');
                    }
                } else {
                    $prod.show().removeClass('d-none');
                }
            } else {
                $prod.hide();
            }
        });

        // Show More düyməsinin görünüşünü idarə edirik
        const filtersActive = searchText.length > 0 || selectedGenres.length > 0 || selectedAuthors.length > 0 ||
            minPrice > 0 || maxPrice < 100000;

        if (filtersActive) {
            $('#showMoreBtn').hide();
        } else {
            $('#showMoreBtn').show();
        }
    }

    // Eventləri bağla
    $('#liveSearchInput').on('input', applyFilters);
    $('.filter-genre').on('change', applyFilters);
    $('.filter-author').on('change', applyFilters);
    $('#priceMin, #priceMax').on('input change', applyFilters);

    // Səhifə açıldıqda filtrləri tətbiq et
    applyFilters();
});
