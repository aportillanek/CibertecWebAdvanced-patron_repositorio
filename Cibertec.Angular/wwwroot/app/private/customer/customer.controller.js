(function () {
    'use strict';
    angular.module('app')
        .controller('customerController', customerController);

    customerController.$inject = ['dataService', 'configService', '$state'];

    function customerController(dataService, configService, $state) {
        var apiUrl = configService.getApiUrl();
        var vm = this;
        vm.customer = {};
        vm.customerList = [];
        vm.modalButtonTitle = '';
        vm.readOnly = false;
        vm.isDelete = false;
        vm.modalTitle = '';
        vm.showCreate = false;
     

        vm.totalRecords = 0;
        vm.currentPage = 1;
        vm.maxSize = 10;
        vm.itemPerPage = 25;

        //funciones
        vm.getCustomer = getCustomer;
        vm.create = create;
        vm.edit = edit;
        vm.delete = customerDelete;
        vm.pageChanged = pageChanged;


        init();
        function init() {
            if (!configService.getLogin()) return $state.go('login');
            configurePagination();
        }
        function configurePagination() {

            var widthScreen = (window.innerWidth > 0) ? window.innerWidth : screen.width;
            if (widthScreen < 420) vm.maxSize = 5;
            totalRecords();
        }
        function pageChanged()
        {
            getPagRecords(vm.currentPage);

        }
        function totalRecords()
        {
            dataService.getData(apiUrl + '/product/count')
                .then(function (result) {
                    vm.totalRecords = result.data;
                    getPageRecords(vm.currentPage);
                }, function (error)
                {
                    vm.customerList = [];
                    console.log(error);
                }
                    );
        }
        function getPageRecords(page)
        {
            dataService.getData(apiUrl + '/customer/list/' + page + '/' + vm.itemPerPage)
                .then(function (result) {
                    vm.customerList = result.data;

                },
                function (error) {
                    vm.customerList = [];
                    console.log(error);
                });
        }

        function getCustomer(id)
        {
            vm.customer = null;
            dataService.getData(apiUrl + '/customer/' + id)
                .then(function (result) {

                    vm.customer = result.data;

                },
                function (error)
                {
                    vm.customer = null;
                    console.log(error);
                }
                    )
        }

        function updateCustomer(customer)
        //function list() {
        //    dataService.getData(apiUrl + '/customer')
        //        .then(function (result) {
        //            vm.customerList = result.data;
        //        },
        //        function (error) {
        //            vm.customerList = [];
        //            console.log(error);
        //        });

        //}


    }

})();