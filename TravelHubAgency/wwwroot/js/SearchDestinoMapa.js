
/*    show mapa del destino    */

async function initMap() {
    map = new google.maps.Map(document.getElementById("mapadestino"), {
        zoom: 14,
        mapTypeId: google.maps.MapTypeId.ROADMAP
    });

    marcador = new google.maps.Marker({
        draggable: true
    });

    movePin();
}

function movePin() {

    var geocoder = new google.maps.Geocoder();
    var searched_destino = $("#destino").val();

    geocoder.geocode({
        "address": searched_destino
    }, function (results, status) {
        if (status == google.maps.GeocoderStatus.OK) {
            map.setCenter(results[0].geometry.location);
            marcador = new google.maps.Marker({
                map: map,
                position: results[0].geometry.location
            });

            console.log("Latitud: ", results[0].geometry.location.lat());
            console.log("Longitud: ", results[0].geometry.location.lng());
            console.log("data: ", results);

            $("#latitud").val(results[0].geometry.location.lat());
            $("#longitud").val(results[0].geometry.location.lng());

        } else {
            console.error("Geocode was not successful for the following reason: " + status);
        }
    });
}