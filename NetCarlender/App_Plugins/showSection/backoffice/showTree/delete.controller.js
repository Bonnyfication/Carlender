angular.module("umbraco").controller("showSection.deleteController", function ($scope, $routeParams, showResource, notificationsService, navigationService) {

    $scope.loaded = false;
    //
    if ($routeParams.id == -1) {
        $scope.show = {};
        $scope.loaded = true;
    }
    else {
        showResource.getById($routeParams.id).then(function (response) {
            $scope.show = response.data;
            $scope.loaded = true;
        });
    }

    $scope.cancelDelete = function () {
        navigationService.hideNavigation();
    }

    $scope.delete = function (show) {
        showResource.deleteById(show).then(function (response) {

            notificationsService.success("Success", "The Show has been deleted.");
            navigationService.hideNavigation();
            navigationService.syncTree({ tree: 'showTree', path: [-1, $scope.id], forceReload: false, activate: true });
        });
    }
});