$(document).ready(function () {

    var host = "http://" + window.location.host;
    var token = null;
    var headers = {};
    var hoteliEndPoint = "/api/hotels/";
    var lanacEndPoint = "/api/lanacs/";
    var editingId;



    $("body").on("click", "#btnDelete", deleteHotel);
    $("body").on("click", "#btnEdit", editHotel);
    loadHoteli();

    function loadHoteli() {
        var requestUrl = host + hoteliEndPoint;
        $.getJSON(requestUrl, setHoteli);
    }


    function setHoteli(data,status) {

        var $container = $("#data");
        $container.empty();

        if (status == "success") {

            var div = $("<div></div>");
            var h1 = $("<h1>Hoteli</h1>");
            div.append(h1);

            var table = $("<table class='table table-bordered'></table>");
            if (token) {
                var header = $("<thead><tr style='background-color:gold'><td>Naziv</td><td>Godina otvaranja</td><td>Broj soba</td><td>Broj zaposlenih</td><td>Lanac</td><td>Akcija</td><td>Akcija</td></tr></thead>");
            } else {
                var header = $("<thead><tr style='background-color:gold'><td>Naziv</td><td>Godina otvaranja</td><td>Broj soba</td><td>Broj zaposlenih</td><td>Lanac</td></tr></thead>");
            }
            table.append(header);

            var tbody = $("<tbody></tbody>");
            for (var i = 0; i < data.length; i++) {

                var row = "<tr>";
                var displayData = "<td>" + data[i].Naziv + "</td><td>" + data[i].GodinaOtvararanja + "</td><td>" + data[i].BrojSoba + "</td><td>" + data[i].BrojZaposlenih + "</td><td>" + data[i].Lanac.Naziv + "</td>";

                var stringId = data[i].Id.toString();
                var displayDelete = "<td><button id=btnDelete name=" + stringId + ">[Brisanje]</button></td>";
                var displayEdit = "<td><button id=btnEdit name=" + stringId + ">[Izmena]</button></td>";
                if (token) {
                    row += displayData + displayDelete + displayEdit + "</tr>";
                } else {
                    row += displayData + "</tr>";
                }
                tbody.append(row);
            }
            table.append(tbody);
            div.append(table);
            $container.append(div);
        } else {
            var div = $("<div></div>");
            var h1 = $("<h1>Hoteli nisu mogli da se ucitaju.</h1>");
            div.append(h1);
            $container.append(div);
        }
    };


    $("#regButton").click(function (e) {
        e.preventDefault();

        $("#registracija").css("display", "block");
        $("#regButton").css("display", "none");
        $("#prijava").css("display", "none");
        $("#priButton").css("display", "none");

    });


    $("#priButton").click(function (e) {
        e.preventDefault();

        $("#registracija").css("display", "none");
        $("#regButton").css("display", "none");
        $("#prijava").css("display", "block");
        $("#priButton").css("display", "none");
        $("#prijavaNaSistem").css("display", "none");
        $("#priEmail").val('');
        $("#priLoz").val('');
    });

    $("#odustajanjeReg").click(function (e) {
        e.preventDefault();

        location.reload();
    });

    $("#odustajanjePri").click(function (e) {
        e.preventDefault();

        location.reload();
    });

    $("#registracija").submit(function (e) {
        e.preventDefault();

        var email = $("#regEmail").val();
        var loz1 = $("#regLoz1").val();
        var loz2 = $("#regLoz2").val();


        var sendData = {
            "Email": email,
            "Password": loz1,
            "ConfirmPassword": loz2
        };

        $.ajax({
            url: host + "/api/Account/Register",
            type: "POST",
            data: sendData
        }).done(function (data) {
            $("#regEmail").val('');
            $("#regLoz1").val('');
            $("#regLoz2").val('');
            $("#info").css("display", "block");
            $("#prijavaNaSistem").css("display", "block");
            $("#prijava").css("display", "none");
           

        }).fail(function (data) {
            alert("Greska prilikom registracije!");
        })
    });


    $("#prijavaNaSistem").click(function (e) {
        e.preventDefault();

        $("#prijava").css("display", "block");
        $("#registracija").css("display", "none");
    });

    $("#prijava").submit(function (e) {
        e.preventDefault();

        var email = $("#priEmail").val();
        var loz = $("#priLoz").val();

        var sendData = {
            "grant_type": "password",
            "username": email,
            "password": loz
        };

        $.ajax({
            "url": host + "/Token",
            "type": "POST",
            "data": sendData
        }).done(function (data) {
            console.log(data);
            $("#info2").empty().append("Prijavljeni korisnik: " + data.userName);
            $("#info2").css("display", "block");
            token = data.access_token;
            $("#data").attr("class", "col-sm-12");
            $("#registracija").css("display", "none");
            $("#prijava").css("display", "none");
            $("#pretraga").css("display", "block");
            $("#odjava").css("display", "block");
            $("#dodavanje").css("display", "block");
            $("#priEmail").val('');
            $("#priLoz").val('');
            loadHoteli();

        }).fail(function (data) {
            alert("Greska prilikom prijave!");
        })
    });

    $("#odjava").click(function (e) {
        e.preventDefault();

        token = null;
        headers = {};
        location.reload();

    });


    $("#izmena").submit(function (e) {
        e.preventDefault();

        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        var lanac1 = $("#lanac1").val();
        var naziv1 = $("#naziv1").val();
        var godinaotvaranja1 = $("#godinaotvaranja1").val();
        var brojsoba1 = $("#brojsoba1").val();
        var brojzaposlenih1 = $("#brojzaposlenih1").val();

        var sendData = {
            "Id": editingId,
            "LanacId": lanac1,
            "Naziv": naziv1,
            "GodinaOtvararanja": godinaotvaranja1,
            "BrojSoba": brojsoba1,
            "BrojZaposlenih": brojzaposlenih1
        };

        $.ajax({
            url: host + hoteliEndPoint + editingId,
            type: "PUT",
            data: sendData,
            headers: headers
        }).done(function (data, status) {
            $("#izmena").css("display", "none");
            $("#dodavanje").css("display", "block");
            loadHoteli();
        }).fail(function (data, status) {
            alert("Greska prilikom izmene!");
        });
    });


    function editHotel() {

        $("#izmena").css("display", "block");
        $("#dodavanje").css("display", "none");

        editingId = this.name;


        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        $.ajax({
            url: host + hoteliEndPoint + editingId.toString(),
            type: "GET",
            headers: headers
        }).done(function (data,status) {
            $("#lanac1").val(data.LanacId);
            $("#naziv1").val(data.Naziv);
            $("#godinaotvaranja1").val(data.GodinaOtvararanja);
            $("#brojsoba1").val(data.BrojSoba);
            $("#brojzaposlenih1").val(data.BrojZaposlenih);
        })
    };

    $.ajax({
        url: host + lanacEndPoint,
        type: "GET"
    }).done(function (data, status) {
        var select = $("#lanac1");
        for (var i = 0; i < data.length; i++) {

            var option = "<option value='" + data[i].Id + "'>" + data[i].Naziv + "</option>";
            select.append(option);
        }
    });

    $("#odustajanjeIzmena").click(function (e) {
        e.preventDefault();

        $("#izmena").css("display", "none");
        $("#dodavanje").css("display", "block");

    });


   
  



    $("#dodavanje").submit(function (e) {
        e.preventDefault();

        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        var lanac = $("#lanac").val();
        var naziv = $("#naziv").val();
        var godinaotvaranja = $("#godinaotvaranja").val();
        var brojsoba = $("#brojsoba").val();
        var brojzaposlenih = $("#brojzaposlenih").val();

        var sendData = {
            "LanacId": lanac,
            "Naziv": naziv,
            "GodinaOtvararanja": godinaotvaranja,
            "BrojSoba": brojsoba,
            "BrojZaposlenih": brojzaposlenih
        };

        $.ajax({
            url: host + hoteliEndPoint,
            type: "POST",
            data: sendData,
            headers: headers
        }).done(function (data, status) {
            $("#lanac").val('1');
            $("#naziv").val('');
            $("#godinaotvaranja").val('');
            $("#brojsoba").val('');
            $("#brojzaposlenih").val('');
            loadHoteli();
        }).fail(function (data, status) {
            alert("Greska prilikom dodavanja!");
        });
    });

    $.ajax({
        url: host + lanacEndPoint,
        type: "GET"
    }).done(function (data, status) {
        var select = $("#lanac");
        for (var i = 0; i < data.length; i++) {

            var option = "<option value='" + data[i].Id + "'>" + data[i].Naziv + "</option>";
            select.append(option);
        }
    });

    function deleteHotel() {

        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        var deleteID = this.name;

        $.ajax({
            url: host + hoteliEndPoint + deleteID.toString(),
            type: "DELETE",
            headers: headers
        }).done(function (data, status) {
            loadHoteli();
        }).fail(function (data, status) {
            alert("Greska prilikom brisanja!");
        });
    };

    $("#pretraga").submit(function (e) {
        e.preventDefault();

        if (token) {
            headers.Authorization = 'Bearer ' + token;
        }

        var mini = $("#od").val();
        var maxi = $("#do").val();

        var sendData = {
            "najmanje": mini,
            "najvise": maxi
        };

        $.ajax({
            url: host + "/api/kapacitet",
            type: "GET",
            data: sendData,
            headers: headers
        }).done(function (data, status) {
            $("#od").val('');
            $("#do").val('');

            setHoteli(data, status);
        }).fail(function (data, status) {
            alert("Greska prilikom pretrage!");
        });
    });


    $("#odustajanjeDodavanje").click(function (e) {
        e.preventDefault();

        $("#lanac").val('1');
        $("#naziv").val('');
        $("#godinaotvaranja").val('');
        $("#brojsoba").val('');
        $("#brojzaposlenih").val('');
    });




});