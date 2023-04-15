// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

$(document).ready(function () {
    $("#data-devolucao").hide();

    $("#DataRetirada").change(function () {
        var data_retirada = $("#DataRetirada").val();
        var data_devolucao = $("#DataDevolucao").get();

        data_devolucao[0].min = data_retirada;

        $("#data-devolucao").show();
    });
});
