(function () {
    'use strict'
    angular.module('app')
        .controller('csvController', loginController);

    function loginController(configService, $state) {
        var vm = this;
        vm.csvLines = [];
        vm.processFile = processFile;
        var fileInput = document.getElementById("csvViewer");
        init();
        function init() {

            if (!configService.getLogin()) $state.go("login");


            //  fileInput.addEventListener('change', readFile);
           
        }

        function processFile()
        {
            vm.csvLines = [];

            readFile(function (result) {
                var list = [];
                var totalLines = result.length;
                var count = 0;
                var csvWorker = new Worker("/js/worker.js");
                csvWorker.addEventListener('message', function (message) {
                    
                    list.push(message.data);
                   
                    console.log("Processing...");
                    count++;
                    if (count >= totalLines) csvWorker.terminate();

                });
                for (var i = 0; i < result.length;i++)
                {
                   
                    csvWorker.postMessage(result[i]);
                }
             
            });


        }

        function readFile(callback)
        {
            var reader = new FileReader();
            //var list = [];
            reader.onload = function ()
            {
               // var lines = reader.result.split("r\n");

                //for (var i = 0; i < lines.length;i++)
                //{
                //    list.push(formatLine(lines[i]))

                //}
                //vm.csvLines.push(list);
                //console.log(list);
               
                return callback(reader.result.split("\r\n"));
            };
            reader.readAsBinaryString(fileInput.files[0]);

        }
        function formatLine(line)
        {
            var splittextline = line.split(",");

            var member = {
                member_no: splittextline[0],
                lastname: splittextline[1],
                firstname: splittextline[2],
                middleinitial: splittextline[3],
                street: splittextline[4],
                city: splittextline[5],
                state_prov: splittextline[6],
                country: splittextline[7],
                mail_code: splittextline[8],
                phone_no: splittextline[9],
                issue_dt: splittextline[10],
                expr_dt: splittextline[11],
                corp_no: splittextline[12],
                prev_balance: splittextline[13],
                curr_balance: splittextline[14],
                member_code: splittextline[15]

            }
            return member;
          
        }


    }




})();