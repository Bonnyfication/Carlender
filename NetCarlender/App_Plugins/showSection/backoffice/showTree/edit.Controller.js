angular.module("umbraco").controller("showSection.editController", function ($scope, $routeParams, showResource, notificationsService, navigationService) {

    $scope.loaded = false;

    // Inhalt wird geladen
    if ($routeParams.id == -1) {
        $scope.show = {};
        $scope.loaded = true;
    }
    else {
        // Lade Artikeldaten
        showResource.getById($routeParams.id).then(function (response) {
            $scope.show = response.data;
            $scope.dateValue = $scope.show.Datum;
            $scope.datepicker.value = $scope.dateValue;

            $scope.loaded = true;
        });
    }

    $scope.save = function (show) {
        $scope.show.Datum = $scope.datepicker.value;

        showResource.save(show).then(function (response) {
            $scope.show = response.data;
            notificationsService.success("Success", show.Titel + " " + " has been saved.");

            navigationService.syncTree({ tree: 'showTree', path: [-1, $scope.id], forceReload: true }).then(function
                (syncArgs) {
                navigationService.reloadNode(syncArgs.node);
            });
        });
    }

    $scope.datepicker = {
        view: 'datepicker',
        config: {
            pickDate: true,
            pickTime: true,
            pick12HourFormat: false,
            format: "DD/MM/YYYY HH:mm"
        },
        value: $scope.dateValue
    };


    $scope.openMediaPicker = function () {
        $scope.mediaPickerOverlay = {
            view: "mediapicker",
            title: "Select Picture",
            startNodeId: 0,
            multiPicker: false, // adjust as per your requirement
            onlyImages: true, // adjust as per your requirement
            disableFolderSelect: true, // adjust as per your requirement
            show: true,
            submit: function (model) {

                $scope.show.Image = model.selectedImages[0].id;
                $scope.selectedImageUrl = model.selectedImages[0].image;

                $scope.mediaPickerOverlay.show = false;
                $scope.mediaPickerOverlay = null;
            }
        };
    }

});
