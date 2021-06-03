//import { fn } from "jquery";

var timeout = null;
$(document).ready(function () {

    $('.datefrom3').datetimepicker({
        format: 'DD/MM/YYYY',
        locale: 'en',
        showTodayButton: true,
        daysOfWeekDisabled: [0, 7],
        useCurrent: true,
        //minDate: mindate,
        showClear: true,
        sideBySide: true,
        stepping: 30,
    });

    GetTop();
    SetHeigth();

    $(document).on("click", ".changegh", function () {
        var elem = $(this);
        var changegh = $(this).attr("changegh");

        $("#hdShipped").val(changegh);

        if (!$(this).hasClass("activex")) {
            $(".changegh").removeClass("activex");
            elem.addClass("activex");
        }

        if (changegh == 1) {
            $("#taicuahang").hide();
            $("#ptnhapdiachi").show();
            //$("#btnHoanThanh").hide();
        }
        else {
            $("#ptnhapdiachi").hide();
            $("#taicuahang").show();

            $('#modalMuaTaiCuaHang').modal({ backdrop: 'static', keyboard: false });

            //$("#btnHoanThanh").show();
        }
    });
    $(document).on("click", "#closewarm", function () {
        $(this).parent().toggle("slow");
    });


    $(document).on("hover", "#goispdon .col-md-3 .divtongd .imggoidon", function () {

        $(this).find("img:first-child").toggle();
        $(this).find("img:last-child").toggle();

        //alert("1");

    });

    $(document).on("click", ".btnGoiDKStyle", function () {

        var xtype = $(this).attr("xtype");

        var kele = $(this).parent().parent().find(".giashipdk");

        if (xtype == 1) {
            kele.find(".gia1").show();
            kele.find(".gia2").hide();
        }
        else {
            kele.find(".gia2").show();
            kele.find(".gia1").hide();
        }

        //alert("1");


    });


    $(document).on("click", "#btnLuuThongTin2", function () {
        var hoten = $("#txtHoTen2").val().trim();
        var sdt = $("#txtSDT2").val().trim();
        var ngaynhan = $("#txtNgayNhan2").val().trim();
        var diachi = "";//$("#txtDiaChi").val().trim();
        var noted = $("#txtNoted2").val().trim();
        var logged = $("#hdLogged").val().trim();
        var styleship = $("#hdShipped").val().trim();

        var timeship = $("#slGioGiaoHang2").children("option:selected").val().replace(':','_');

        if (hoten == "" || sdt == "" || ngaynhan == "") {
            $('#modalWarning').modal({ backdrop: 'static', keyboard: false });
        }
        else {
            $.ajax({
                url: '/home/InsertDonHang',
                contentType: 'application/json; charset=utf-8',
                data: { fname: hoten, sdt: sdt, ngaygiao: ngaynhan, dchi: diachi, ghichu: noted, logged: logged, styleship: styleship, timeship: timeship },
                type: 'GET',
                dataType: 'json'
                , success: function (data) {

                    if (data != "0") {


                        $(".inputgiaohang").hide();
                        $(".thongtinthanhtoan").show();

                        $(".circlenumber").parent().parent().removeClass("actived");
                        $(".circlenumber").removeClass("activeds");
                        $(".cc3").parent().parent().addClass("actived");
                        $(".cc3").addClass("activeds");

                        $("#hdCartID").val(data);
                        $("#btnXacMinhTK").hide();
                        $("#btnChonGiaoHang").hide();
                        $("#btnHoanThanh").show();

                        $("#modalMuaTaiCuaHang").modal('toggle');
                    }

                },
                error: function (xhr, status) {
                    alert("Fail connect to system server. Please try again or check internet connection.");
                },
                complete: function (xhr, status) {

                }
            });
        }

    });




    $(document).on("click", "#btnLuuThongTin", function () {
        var hoten = $("#txtHoTen").val().trim();
        var sdt = $("#txtSDT").val().trim();
        var ngaynhan = $("#txtNgayNhan").val().trim();
        var diachi = $("#txtDiaChi").val().trim();
        var noted = $("#txtNoted").val().trim();
        var logged = $("#hdLogged").val().trim();
        var styleship = $("#hdShipped").val().trim();

        var timeship = $("#slGioGiaoHang").children("option:selected").val().replace(':', '_');

        if (hoten == "" || sdt == "" || ngaynhan == "" || diachi == "") {
            $('#modalWarning').modal({ backdrop: 'static', keyboard: false });
        }
        else {
            $.ajax({
                url: '/home/InsertDonHang',
                contentType: 'application/json; charset=utf-8',
                data: { fname: hoten, sdt: sdt, ngaygiao: ngaynhan, dchi: diachi, ghichu: noted, logged: logged, styleship: styleship, timeship: timeship },
                type: 'GET',
                dataType: 'json'
                , success: function (data) {

                    if (data != "0") {
                        $(".inputgiaohang").hide();
                        $(".thongtinthanhtoan").show();

                        $(".circlenumber").parent().parent().removeClass("actived");
                        $(".circlenumber").removeClass("activeds");
                        $(".cc3").parent().parent().addClass("actived");
                        $(".cc3").addClass("activeds");

                        $("#hdCartID").val(data);
                        $("#btnXacMinhTK").hide();
                        $("#btnChonGiaoHang").hide();
                        $("#btnHoanThanh").show();
                    }
                   
                },
                error: function (xhr, status) {
                    alert("Fail connect to system server. Please try again or check internet connection.");
                },
                complete: function (xhr, status) {
                    
                }
            });
        }

    });

    $(document).on("click", "#btnHoanThanh", function () {

        var cartid = $("#hdCartID").val();

        $.ajax({
            url: '/home/CompleteCart',
            contentType: 'application/json; charset=utf-8',
            data: { cartid: cartid },
            type: 'GET',
            dataType: 'json'
            , success: function (data) {

                if (data == true) {
                    location.href = "/home/orderdetail";
                }

            },
            error: function (xhr, status) {
                alert("Fail connect to system server. Please try again or check internet connection.");
            },
            complete: function (xhr, status) {

            }
        });

    });

    $(document).on("click", "#linklogin", function () {
        //$('#modalLogin').modal({ backdrop: 'static', keyboard: false });
    });

    $(document).on("click", "#btnTuVanDinhKy", function () {

        $('#modalTuVan').modal({ backdrop: 'static', keyboard: false });

    });
    $(document).on("click", "#btnTuVanDinhKy2", function () {

        $('#modalTuVan').modal({ backdrop: 'static', keyboard: false });

    });
    $(document).on("click", ".closed", function () {

        $("#full-opacity").hide();
        $(".minicart").hide();

        $("body").css("overflow-y", "auto");
    });

    $(document).on("click", ".btnAddCartGoi", function () {

        var productid = $(this).attr("productid");

        var catespdon = 1;

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
                        data: {},
                        type: 'GET',
                        headers: { 'Access-Control-Allow-Origin': '*' },
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


                            $("#full-opacity").show();
                            $(".minicart").show();
                            $("body").css("overflow-y", "hidden");

                            updateiconcart();

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

    $(document).on("click", "#btnHuy2", function () {

        $("#modalMuaTaiCuaHang").modal('toggle');

    });

    $(document).on("click", "#btnHuy", function () {

        $("#lstfullgiohang").show();
        $(".inputgiaohang").hide();
        $("#btnChonGiaoHang").show();
        $("#btnXacMinhTK").hide();
        $("#btnHoanThanh").hide();

        $(".circlenumber").parent().parent().removeClass("actived");
        $(".circlenumber").removeClass("activeds");
        $(".cc1").parent().parent().addClass("actived");
        $(".cc1").addClass("activeds");

    });

    $(document).on("click", "#btnChonGiaoHang", function () {

        $("#lstfullgiohang").hide();
        $(".inputgiaohang").show();
        $(this).hide();
        var logged = $("#hdLogged").val().trim();

        if (logged == "0") {
            //$("#lstfullgiohang").hide();
            //$(".inputgiaohang").show();
            //$(this).hide();
            $("#btnXacMinhTK").show();
            $("#btnHoanThanh").hide();
        }
        else {
            
            $("#btnXacMinhTK").hide();
            //$("#btnHoanThanh").show();
        }

        

        $(".circlenumber").parent().parent().removeClass("actived");
        $(".circlenumber").removeClass("activeds");
        $(".cc2").parent().parent().addClass("actived");
        $(".cc2").addClass("activeds");
    });

    $(document).on("click", "#btnXacMinhTK", function () {

        location.href = "/account/logincust";

    });

    $(document).on("click", ".btnRemoveCart2", function () {

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
                        element.parent().parent().remove();
                        $("#totaltien").text(k[1]);

                        var cartpid = "cartp_" + productid;
                        $("#tongfullgiohang").find("." + cartpid + "").remove();

                        if (k[1] == "0") {
                            location.reload();
                        }
                        
                    }
                },
                error: function (xhr, status) {
                    alert("Fail connect to system server. Please try again or check internet connection.");
                },
                complete: function (xhr, status) {
                    var numbertotal = parseInt($(".numbersl").text().trim());

                    $(".numbersl").text(numbertotal - 1);

                    updateiconcart();
                }
            });
        }
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

                    updateiconcart();
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
                        elem.parent().parent().parent().find(".cartitemright .itempricefc span").text(k[2]);

                        var qty = parseInt(elem.parent().parent().find(".form-inline .txtSoLuongDH").val());
                        var cartpid = "cartp_" + productid;
                        $("#tongfullgiohang").find("." + cartpid + " .totalleft .nhansl span").text(qty);
                        var xxx = k[2].replace(',', '');            
                        var xfinal = formatCurrency(qty * parseInt(xxx)).replace('.0', '');
                        $("#tongfullgiohang").find("." + cartpid + " .totalright span span.tprice").text(xfinal);
                    }



                },
                error: function (xhr, status) {
                    alert("Fail connect to system server. Please try again or check internet connection.");
                },
                complete: function (xhr, status) {
                    elem.parent().find(".changeml").removeClass("actived");
                    elem.addClass("actived");
                    updateiconcart();
                }
            });

        }
        
    });

    $(document).on("change", 'input[type=radio][name=optionsRadios]', function () {

        if (this.value == '1') {
            $("#hdCateSpDon").val("1");
        }
        else {
            $("#hdCateSpDon").val("2");
        }
    });

    $(document).on("click", ".loadx", function () {

        alert("s");

    });

    $(document).on("click", "#btnTruSL", function () {

        var sl = parseInt($("#txtSoLuongDHThem").val().trim());
        var xx = sl - 1;
        if (xx <= 1) {
            xx = 1;
        }
        $("#txtSoLuongDHThem").val(xx);

    });

    $(document).on("click", "#btnCongSL", function () {

        var sl = parseInt($("#txtSoLuongDHThem").val().trim());
        var xx = sl + 1;
        if (xx >= 100) {
            xx = 100;
        }
        $("#txtSoLuongDHThem").val(xx);

    });

    $(document).on("click", "#btnThemGioHang", function () {

        var productid = $(this).attr("productid");

        var catespdon = $("#hdCateSpDon").val().trim();

        var quantity = $("#txtSoLuongDHThem").val().trim();

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


                            $("#full-opacity").show();
                            $(".minicart").show();
                            $("body").css("overflow-y", "hidden");

                            updateiconcart();

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

    $(document).on("click", "#leftql li", function () {
        var id = $(this).attr("cartid");
        $.ajax({
            url: '/home/GetDetailOrder',
            contentType: 'application/html; charset=utf-8',
            data: { id: id },
            type: 'GET',
            //cache: false,
            dataType: 'html'
            , success: function (data) {

                $("#rightql").html(data);

            },
            error: function (xhr, status) {


                alert("Fail connect to system server. Please try again or check internet connection.");

            },
            complete: function (xhr, status) {



            }
        });


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


                            $("#full-opacity").show();
                            $(".minicart").show();
                            $("body").css("overflow-y", "hidden");

                            updateiconcart();

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
        var elem = $(this);
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


                            var qty = parseInt(elem.parent().find(".txtSoLuongDH").val());
                            var cartpid = "cartp_" + productid;
                            $("#tongfullgiohang").find("." + cartpid + " .totalleft .nhansl span").text(qty);
                            var ppp = elem.parent().parent().parent().find(".cartitemright .itempricefc span").text().trim();
                            var xxx = ppp.split(',').join('');
                            var xfinal = formatCurrency(qty * parseInt(xxx)).replace('.0', '');
                            $("#tongfullgiohang").find("." + cartpid + " .totalright span span.tprice").text(xfinal);
                        }

                    },
                    error: function (xhr, status) {
                        alert("Fail connect to system server. Please try again or check internet connection.");
                    },
                    complete: function (xhr, status) {
                        updateiconcart();
                    }
                });
            }

        }, 1000);      
    });

    $(document).on("click", "#btnDatHangMini", function () {

        location.href = "/home/cart";

    });

    $(document).on("focusout", "#txtAccount", function () {

        var uname = $(this).val().trim();

        $.ajax({
            url: '/home/CheckUserName',
            contentType: 'application/html; charset=utf-8',
            data: { uname: uname },
            type: 'GET',
            dataType: 'json'
            , success: function (data) {

                if (data == "1") {
                    $("#ckUsername").val("1");
                    $('#modalWarning3').modal({ backdrop: 'static', keyboard: false });
                }
                else {
                    $("#ckUsername").val("0");
                }

            },
            error: function (xhr, status) {
                alert("Fail connect to system server. Please try again or check internet connection.");
            },
            complete: function (xhr, status) {
                
            }
        });


    });

    $('#modalWarning4').on('hidden.bs.modal', function () {
        location.href = "/home";
    })

    $(document).on("click", "#btnDangKy", function () {

        var uname = $("#txtAccount").val().trim();
        var ckname = $("#ckUsername").val().trim();
        var pword = $("#txtHassPass").val().trim();
        var cfirm = $("#txtConfirm").val().trim();
        var email = $("#txtEmail").val().trim();
        var mobile = $("#txtMobile").val().trim();
        var fname = $("#txtFullname").val().trim();
        var dchi = $("#textdiachi").val().trim();

        if (ckname == "0" && pword != "" && email != "" && mobile != "" && fname != "") {
            if (pword == cfirm) {

                $.ajax({
                    url: '/home/InsertUser',
                    contentType: 'application/html; charset=utf-8',
                    data: { uname: uname, pword: pword, email: email, mobile: mobile, fname: fname, dchi: dchi },
                    type: 'GET',
                    dataType: 'json'
                    , success: function (data) {

                        if (data == true) {
                            $('#modalWarning4').modal({ backdrop: 'static', keyboard: false });
                        }
                        else {
                            alert("Đăng ký thất bại ! Vui lòng thử lại ");
                        }

                    },
                    error: function (xhr, status) {
                        alert("Fail connect to system server. Please try again or check internet connection.");
                    },
                    complete: function (xhr, status) {

                    }
                });


            }
            else {
                $('#modalWarning2').modal({ backdrop: 'static', keyboard: false });
            }
        }
        else if (ckname == "1")
        {
            $('#modalWarning3').modal({ backdrop: 'static', keyboard: false });
        }
        else {
            $('#modalWarning').modal({ backdrop: 'static', keyboard: false });
        }

    });


    $(document).on("click", ".btnCongSL", function () {

        var elem = $(this);

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


                        var qty = parseInt(elem.parent().find(".txtSoLuongDH").val());
                        var cartpid = "cartp_" + productid;
                        $("#tongfullgiohang").find("." + cartpid + " .totalleft .nhansl span").text(qty);
                        var ppp = elem.parent().parent().parent().find(".cartitemright .itempricefc span").text().trim();
                        var xxx = ppp.split(',').join('');
                        var xfinal = formatCurrency(qty * parseInt(xxx)).replace('.0', '');
                        $("#tongfullgiohang").find("." + cartpid + " .totalright span span.tprice").text(xfinal);


                    }
                },
                error: function (xhr, status) {


                    alert("Fail connect to system server. Please try again or check internet connection.");

                },
                complete: function (xhr, status) {
                    updateiconcart();
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

    $('.popper').popover({
        placement: 'top',
        trigger: 'hover',
        container: 'body',
        title: 'Phí vận chuyển',
        html: true,
        content: function () {
            return $('.popper-content').html();
        }
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

        ]
    });

    updateimagedata();

});

function updateiconcart() {
    var numbersl = $(".numbersl").text().trim();

    $(".redshop").text(numbersl);
}

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
    $("#leftql").css("min-height", hw - 70);
    $("#leftql").css("max-height", hw - 70);
    $("#leftql ul").css("max-height", hw - 240);

    //alert("sd");
}

function formatCurrency(total) {
    var neg = false;
    if (total < 0) {
        neg = true;
        total = Math.abs(total);
    }
    return (neg ? "-$" : '') + parseFloat(total, 10).toFixed(1).replace(/(\d)(?=(\d{3})+\.)/g, "$1,").toString();
}

function GetRating() {
    var xx = $("#classrating").text().trim();
    var rate = parseInt(xx);
    var html = "";
    var countview = parseInt($("#classviewcount").text().trim());
    for (var i = 0; i < rate; i++) {
        html += "<i class='glyphicon glyphicon-star goldstar'></i>";
    }

    for (var i = 0; i < 5 - rate; i++) {
        html += "<i class='glyphicon glyphicon-star-empty'></i>";
    }

    html += "<span id='viewcountdx'>(" + countview + " lượt đánh giá)</span>"

    if (rate == 0 || xx == "") {
        html = "<span id='chuarating'>Chưa có đánh giá</span>"
    }

    $(".ratingx").append(html);

}

function updateimagedata() {
    var him = $("#goispdon .col-md-3 .divtongd .imggoidon img:first-child").innerWidth();

    $("#goispdon .col-md-3 .divtongd .imggoidon img").css("height", him);
}