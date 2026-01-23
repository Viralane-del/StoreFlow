(function ($) {
    'use strict';
    $(function () {
        $('#order-listing').DataTable({
            "aLengthMenu": [
                [5, 10, 15, -1],
                [5, 10, 15, "Tümü"]
            ],
            "iDisplayLength": 10,
            "language": {
                "search": "",
                "searchPlaceholder": "Ara...",
                "lengthMenu": "_MENU_ kayıt göster",
                "zeroRecords": "Kayıt bulunamadı",
                "info": "_TOTAL_ kayıttan _START_ - _END_ arası gösteriliyor",
                "infoEmpty": "Kayıt yok",
                "infoFiltered": "(_MAX_ kayıt içerisinden filtrelendi)",
                "paginate": {
                    "first": "İlk",
                    "last": "Son",
                    "next": "›",
                    "previous": "‹"
                }
            }
        });

        $('#order-listing').each(function () {
            var datatable = $(this);

            // SEARCH input
            var search_input = datatable
                .closest('.dataTables_wrapper')
                .find('div[id$=_filter] input');
            search_input.attr('placeholder', 'Ara...');
            search_input.removeClass('form-control-sm');

            // LENGTH select
            var length_sel = datatable
                .closest('.dataTables_wrapper')
                .find('div[id$=_length] select');
            length_sel.removeClass('form-control-sm');
        });
    });
})(jQuery);
