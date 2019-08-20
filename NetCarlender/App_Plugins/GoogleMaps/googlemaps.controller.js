angular.module("umbraco").controller("Plugin.GoogleMapsController", function ($scope, assetsService)
{
    

        if (!$scope.model.value) {
            $scope.model.value = {};

            startLat = 29.50000;
            startLong = 19.59000;
        } else {
            startLat = $scope.model.value.latitude;
            startLong = $scope.model.value.longitude;
        }



        assetsService.loadJs("http://www.google.com/jsapi")
            .then(function () {
                google.load("maps", "3", { callback: initialize, other_params: "sensor=false" });
                console.log("google.laoad() executed");
            });

        function initialize() {
            geocoder = new google.maps.Geocoder();
            var myLatlng = new google.maps.LatLng(startLat, startLong);
            var mapOptions = { zoom: 4, center: myLatlng };
            map = new google.maps.Map(document.getElementById('myGoogleMapsContainer'), mapOptions);

            marker = new google.maps.Marker({
                position: myLatlng, map: map, title: 'Hello World!', draggable: true
            });

            console.log("marker created");

            google.maps.event.addListener(marker, "position_changed", function (e) {
                var position = marker.getPosition();
                $scope.model.value.latitude = position.lat();
                $scope.model.value.longitude = position.lng();

                console.log("marker moved");
            });
    }





});
