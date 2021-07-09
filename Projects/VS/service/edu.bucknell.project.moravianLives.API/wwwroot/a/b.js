angular.module('personApp', [])
    .controller('personController', ["$http", function ($http) {
        var personList = this;
        personList.endpoint = "data/person";
        personList.persons = [];
        personList.fetchperson = function () {
            console.log("fetch");
            $http.get('/data/person').then(function (response) {
                personList.persons = response.data;
            });
        }

        personList.fetchperson(); 

    }]);