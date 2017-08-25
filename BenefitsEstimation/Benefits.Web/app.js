Vue.use(VueMaterial);

var app = new Vue({
    el: '#app',
    data: {
        title: 'Benefits Cost Estimator',
        hub: {},
        benefitQuote: {},
        //Commands
        addEmployee: {
            firstName: "",
            lastName: ""
        },
        addSpouse: {
            firstName: "",
            lastName: ""
        },
        addDependent: {
            firstName: "",
            lastName: ""
        }
    },
    methods: {
        openDialog(ref) {
            this.$refs[ref].open();
        },
        closeEmployeeDialog(ref, shouldSave) {
            this.$refs[ref].close();
            if (shouldSave) {
                this.addEmployee.id = this.hub.connection.id;
                this.hub.server.addEmployee(this.addEmployee);
            }
        },
        closeSpouseDialog(ref, shouldSave) {
            this.$refs[ref].close();
            if (shouldSave) {
                this.addSpouse.id = this.hub.connection.id;
                this.hub.server.addSpouse(this.addSpouse);
            }
        },
        closeDependentDialog(ref, shouldSave) {
            this.$refs[ref].close();
            if (shouldSave) {
                this.addDependent.id = this.hub.connection.id;
                this.hub.server.addDependent(this.addDependent);
            }
        }
    },
    mounted: function () {
        this.hub = $.connection.benefitQuoteHub;
        $.connection.hub.start();
        this.hub.client.updateEstimate = function (data) {
            app.benefitQuote = data;
        };
    }
});

