$(function () {
    console.log("Called");

    $('.table-data').each(function () {
        var tableId = $(this).data('tableid');
        var moreId = $(this).data('moreid');
        var lessId = $(this).data('lessid');

        $(`#${tableId}`).on('show.bs.collapse', function () {
            $(`#${moreId}`).hide();
            $(`#${lessId}`).show();
        });

        $(`#${tableId}`).on('hide.bs.collapse', function () {
            $(`#${moreId}`).show();
            $(`#${lessId}`).hide();
        });
    });
});