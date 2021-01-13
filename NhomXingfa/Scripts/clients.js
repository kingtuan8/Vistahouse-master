
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

        //alert("jdd");

        $("#full-opacity").toggle("slow");
        $(".minicart").toggle("slow");

        $("body").css("overflow-y", "hidden");

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

    $(document).on("click", "#btnTruSL", function () {

        var parentx = $(this).parent();

        var sl = parseInt(parentx.find("#txtSoLuongDH").val().trim());

        var tru = sl - 1;

        if (tru <= 1) {
            tru = 1;
        }        

        parentx.find("#txtSoLuongDH").val(tru);

    });

    $(document).on("click", "#btnCongSL", function () {

        var parentx = $(this).parent();

        var sl = parseInt(parentx.find("#txtSoLuongDH").val().trim());

        var cong = sl + 1;

        if (cong >= 100) {
            cong = 100;
        }


        parentx.find("#txtSoLuongDH").val(cong);

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
    $("#giohangmini").css("min-height", hw);
}