var timeout = null;
$(document).ready(function () {

    GetTop();
    SetHeigth();

    $(document).on("click", "#btnTuVanDinhKy", function () {

        $('#modalTuVan').modal({ backdrop: 'static', keyboard: false });

    });
    $(document).on("click", ".closed", function () {

        $("#full-opacity").toggle("slow");
        $(".minicart").toggle("slow");

        $("body").css("overflow-y", "auto");
    });

    $(document).on("click", ".btnAddCartGoi", function () {

        var productid = $(this).attr("productid");

        var catespdon = 0;

        var quantity = 0;

        $.ajax({
            url: '/home/AddCartJson',
            contentType: 'application/json; charset=utf-8',
            data: { productid: productid, catespdon: catespdon, quantity: quantity},
            type: 'GET',
            //cache: false,
            dataType: 'json'
            , success: function (data) {


                if (data.split('_')[0] == "ok") {


                    $.ajax({
                        url: '/home/AddCartData',
                        contentType: 'application/html; charset=utf-8',
                        data: {  },
                        type: 'GET',
                        //cache: false,
                        dataType: 'html'
                        , success: function (data) {

                            $("#showminicart").html(data);

                        },
                        error: function (xhr, status) {


                            alert("Fail connect to system server. Please try again or check internet connection.");

                        },
                        complete: function (xhr, status) {

                            var hw = window.innerHeight;

                            $("#full-opacity").css("height", hw);
                            $(".minicart").css("height", hw);
                            $(".minicart").css("max-height", hw);
                            $(".listdathang").css("height", hw - 70);
                            $(".listdathang").css("max-height", hw - 70 - 180);
                            $("#giohangmini").css("min-height", hw);


                            $("#full-opacity").toggle("slow");
                            $(".minicart").toggle("slow");
                            $("body").css("overflow-y", "hidden");

                        }
                    });

                }


            },
            error: function (xhr, status) {


                alert("Fail connect to system server. Please try again or check internet connection.");

            },
            complete: function (xhr, status) {


              
            }
        });
    });

    $(document).on("click", ".btnRemoveCart", function () {

        var element = $(this);
        var productid = $(this).attr("productid");

        var conf = confirm("Bạn có chắc chắn muốn xóa sản phẩm này !");
        var quantity = 0;
        if (conf) {
            $.ajax({
                url: '/home/RemoveCart',
                contentType: 'application/html; charset=utf-8',
                data: { productid: productid, quantity: quantity },
                type: 'GET',
                dataType: 'json'
                , success: function (data) {

                    var k = data.split('_');

                    if (k[0] == "ok") {
                        element.parent().parent().parent().parent().remove();
                        $("#totaltien").text(k[1]);
                    }
                  


                },
                error: function (xhr, status) {
                    alert("Fail connect to system server. Please try again or check internet connection.");
                },
                complete: function (xhr, status) {
                    var numbertotal = parseInt($(".numbersl").text().trim());

                    $(".numbersl").text(numbertotal - 1);
                }
            });
        }

        

    });

    $(document).on("click", ".changeml", function () {
        var elem = $(this);
        var productid = $(this).parent().find(".piddata").text().trim();
        var cml = $(this).attr("changeml");
        if (!$(this).hasClass('actived')) {
            

            $.ajax({
                url: '/home/ChangeML',
                contentType: 'application/html; charset=utf-8',
                data: { productid: productid, catespdon: cml },
                type: 'GET',
                dataType: 'json'
                , success: function (data) {

                    var k = data.split('_');

                    if (k[0] == "ok") {
                        $("#totaltien").text(k[1]);
                        elem.parent().parent().parent().parent().find(".col-md-3 .cartprice span").text(k[2]);
                    }



                },
                error: function (xhr, status) {
                    alert("Fail connect to system server. Please try again or check internet connection.");
                },
                complete: function (xhr, status) {
                    elem.parent().find(".changeml").removeClass("actived");
                    elem.addClass("actived");
                }
            });

        }
        
    });

    $(document).on("click", ".btnAddCartDon", function () {

        var productid = $(this).attr("productid");

        var catespdon = 1;

        var quantity = 0;

        $.ajax({
            url: '/home/AddCartJson',
            contentType: 'application/json; charset=utf-8',
            data: { productid: productid, catespdon: catespdon, quantity: quantity },
            type: 'GET',
            //cache: false,
            dataType: 'json'
            , success: function (data) {


                if (data.split('_')[0] == "ok") {


                    $.ajax({
                        url: '/home/AddCartData',
                        contentType: 'application/html; charset=utf-8',
                        data: {},
                        type: 'GET',
                        //cache: false,
                        dataType: 'html'
                        , success: function (data) {

                            $("#showminicart").html(data);

                        },
                        error: function (xhr, status) {


                            alert("Fail connect to system server. Please try again or check internet connection.");

                        },
                        complete: function (xhr, status) {

                            var hw = window.innerHeight;

                            $("#full-opacity").css("height", hw);
                            $(".minicart").css("height", hw);
                            $(".minicart").css("max-height", hw);
                            $(".listdathang").css("height", hw - 70);
                            $(".listdathang").css("max-height", hw - 70 - 180);
                            $("#giohangmini").css("min-height", hw);


                            $("#full-opacity").toggle("slow");
                            $(".minicart").toggle("slow");
                            $("body").css("overflow-y", "hidden");

                        }
                    });

                }


            },
            error: function (xhr, status) {


                alert("Fail connect to system server. Please try again or check internet connection.");

            },
            complete: function (xhr, status) {



            }
        });
        

    });

    $(window).on('scroll', function () {

        GetTop();
        
    });

    $(document).on("click", ".ytxt #yticon img", function () {

        $(".ytxt").removeClass("ytframe");
        $(".ytxt").addClass("ytframe2");

        $(".ytxt #yticon").hide();

        $("#frameyt").show();

    });

    $(document).on("click", "#ytremove", function () {
        $(".ytxt").removeClass("ytframe2");
        $(".ytxt").addClass("ytframe");

        $(".ytxt #yticon").show();

        $("#frameyt").hide();
    });

    $(document).on("click", ".btnTruSL", function () {

        var productid = $(this).attr("productid");
        //var catesp = $(this).attr("catesp");

        var parentx = $(this).parent();

        var sl = parseInt(parentx.find(".txtSoLuongDH").val().trim());

        var tru = sl - 1;

        if (tru <= 1) {
            tru = 1;
        }        

        parentx.find(".txtSoLuongDH").val(tru);

        clearTimeout(timeout);

        timeout = setTimeout(function () {

            if (sl > 1) {
                $.ajax({
                    url: '/home/RemoveCart',
                    contentType: 'application/html; charset=utf-8',
                    data: { productid: productid, quantity: tru },
                    type: 'GET',
                    dataType: 'json'
                    , success: function (data) {

                        var k = data.split('_');

                        if (k[0] == "ok") {
                            $("#totaltien").text(k[1]);
                        }

                    },
                    error: function (xhr, status) {
                        alert("Fail connect to system server. Please try again or check internet connection.");
                    },
                    complete: function (xhr, status) {

                    }
                });
            }

        }, 1000);

        


    });

    $(document).on("click", ".btnCongSL", function () {

        var productid = $(this).attr("productid");
        var catesp = $(this).attr("catesp");

        var parentx = $(this).parent();

        var sl = parseInt(parentx.find(".txtSoLuongDH").val().trim());

        var cong = sl + 1;

        if (cong >= 100) {
            cong = 100;
        }
        parentx.find(".txtSoLuongDH").val(cong);

        clearTimeout(timeout);

        timeout = setTimeout(function () {

            $.ajax({
                url: '/home/AddCartJson',
                contentType: 'application/json; charset=utf-8',
                data: { productid: productid, catespdon: catesp, quantity: cong },
                type: 'GET',
                //cache: false,
                dataType: 'json'
                , success: function (data) {
                    var k = data.split('_');

                    if (k[0] == "ok") {
                        $("#totaltien").text(k[1]);
                    }
                },
                error: function (xhr, status) {


                    alert("Fail connect to system server. Please try again or check internet connection.");

                },
                complete: function (xhr, status) {
                }
            });

        }, 1000);

    });


    $(document).on("click", ".titlecauhoi", function () {

        var pare = $(this).parent();

        

        if (pare.find(".noidungtraloi").is(':hidden')) {

            $(".titlecauhoi i").removeClass("glyphicon-triangle-bottom");
            $(".titlecauhoi i").addClass("glyphicon-triangle-right");
            

            $(".noidungtraloi").hide();

            pare.find(".titlecauhoi i").removeClass("glyphicon-triangle-right");
            pare.find(".titlecauhoi i").addClass("glyphicon-triangle-bottom");
            pare.find(".noidungtraloi").fadeIn('fast');
        }
        else {

            pare.find(".titlecauhoi i").removeClass("glyphicon-triangle-bottom");
            pare.find(".titlecauhoi i").addClass("glyphicon-triangle-right");

            pare.find(".noidungtraloi").hide();

            //alert("2");
        }
        


    });

    $(".variable").slick({
        dots: true,
        infinite: true,
        autoplay: true,
        autoplaySpeed: 5000
    });

    


    $('.sliderthuonghieu').slick({
        dots: false,
        infinite: true,
        speed: 300,
        slidesToShow: 4,
        slidesToScroll: 4,
        responsive: [
            {
                breakpoint: 1024,
                settings: {
                    slidesToShow: 3,
                    slidesToScroll: 3,
                    infinite: true,
                    dots: true
                }
            },
            {
                breakpoint: 600,
                settings: {
                    slidesToShow: 2,
                    slidesToScroll: 2
                }
            },
            {
                breakpoint: 480,
                settings: {
                    slidesToShow: 1,
                    slidesToScroll: 1
                }
            }
            // You can unslick at a given breakpoint now by adding:
            // settings: "unslick"
            // instead of a settings object
        ]
    });


});

function GetTop() {
    var x = $(window).scrollTop();
    if (x >= 30) {
        $(".navbar-fixed-top").css("top", 0);
        $("body").css("padding-top", 55);
    }
    else {
        $(".navbar-fixed-top").css("top", 25);
        $("body").css("padding-top", 80);
    }
}


function SetHeigth() {

    var h = $(document).height();
    var w = window.innerWidth;
    var hw = window.innerHeight;

    $("#full-opacity").css("height", hw);
    $(".minicart").css("height", hw);
    $(".minicart").css("max-height", hw);
    $(".listdathang").css("height", hw - 70);
    $(".listdathang").css("max-height", hw - 70 - 180);
    $("#giohangmini").css("min-height", hw);

    //alert("sd");
}