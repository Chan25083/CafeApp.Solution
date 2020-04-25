$(function () {
$(".foodBtn").click(function () {
    var menuId = $(this).val();
    $.ajax({
        type: "POST",
        url: "/OrderFood/OrderFood",
        data: { "menuId": menuId }
    })
})

    $(".addUnite").click(function () {
        var cartId = $(this).val();
        $.ajax({
            type: "POST",
            url: "/OrderFood/AddUnite",
            data: { "cartId": cartId }
        })
    })

    $(".minusUnit").click(function () {
        var cartId = $(this).val();
        $.ajax({
            type: "POST",
            url: "/OrderFood/MinusUnite",
            data: { "cartId": cartId }
        })
    })

    $(".deleteOrder").click(function () {
        var cartId = $(this).val();
        $.ajax({
            type: "POST",
            url: "/OrderFood/DeleteOrder",
            data: { "cartId": cartId }
        })
    })
})