angular.module("umbraco").controller("showSection.editController", function ($scope, $routeParams, showResource, notificationsService, navigationService) {

    $scope.loaded = false;

    // Inhalt wird geladen
    if ($routeParams.id == -1) {
        $scope.show = {};
        $scope.loaded = true;
    }
    else {
        // Lade Carshow Daten
        showResource.getById($routeParams.id).then(function (response) {
            $scope.show = response.data;

            // SET DATE
            $scope.datepicker.value = $scope.show.Datum;
            $scope.convertedDate = $scope.show.Datum.replace("T", " ");
            $('#carshowdate').val($scope.convertedDate);
            $('#carshowdate').change();

            // SET DESCRIPTION
            $scope.richEditor.value = $scope.show.Beschreibung;

            // SET IMAGE
            $scope.selectedImageUrl = $scope.show.Imagepath;
            

            $scope.loaded = true;
        });
    }

    // SAVE ACTION
    $scope.save = function (show) {
        // GET DATUM
        $scope.show.Datum = $scope.datepicker.value;
        // GET BESCHREIBUNG
        $scope.show.Beschreibung = $scope.richEditor.value;

        if ($scope.datepicker.value == null || $scope.show.Image == 'undefined') {
            notificationsService.error("Failed", show.Titel + " " + " has not been saved.");
            notificationsService.error("Failed", "Please fill mising data!");
        } else {
            showResource.save(show).then(function (response) {
                $scope.show = response.data;
                notificationsService.success("Success", show.Titel + " " + " has been saved.");


                // Confirm object isnt dirty anymore
                $scope.carshowForm.$dirty = false;

                navigationService.syncTree({ tree: 'showTree', path: [-1, $scope.id], forceReload: true }).then(function
                    (syncArgs) {
                    navigationService.reloadNode(syncArgs.node);
                });
            });
        }
        


    }

    // DATEPICKER

    $scope.datepicker = {
        editor: "Umbraco.DateTime",
        label: 'Carshow Datum',
        description: 'carshow datepicker',
        hideLabel: false,
        view: 'datepicker',
        alias: 'carshowdate',
        value: null,
        config: {
            pickDate: true,
            pickTime: true,
            pick12HourFormat: false,
            format: "YYYY-MM-DD HH:mm:ss",
            icons: {
                time: "icon-time",
                date: "icon-calendar",
                up: "icon-chevron-up",
                down: "icon-chevron-down"
            }
        }
    };

    // MEDIAPICKER
    $scope.openMediaPicker = function () {
        $scope.mediaPickerOverlay = {
            view: "mediapicker",
            title: "Select Picture",
            startNodeId: 0,
            multiPicker: false, 
            onlyImages: true, 
            disableFolderSelect: true,
            show: true,
            submit: function (model) {

                $scope.show.Image = model.selectedImages[0].id;
                $scope.show.Imagepath = model.selectedImages[0].image;
                $scope.selectedImageUrl = model.selectedImages[0].image;

                $scope.mediaPickerOverlay.show = false;
                $scope.mediaPickerOverlay = null;
            }
        };
    }

    // RICH EDITOR
    $scope.richEditor = {
        label: 'Carshow Description Editor',
        description: 'Carshow Description',
        view: 'rte',
        value: '',
        config: {
            editor: {
                toolbar: ["code", "undo", "redo", "cut", "styleselect", "bold", "italic", "alignleft", "aligncenter", "alignright", "bullist", "numlist", "link", "umbmediapicker", "umbmacro", "umbembeddialog"],
                stylesheets: ["rte.css"],
                dimensions: {}
            }
        }
    };

});
