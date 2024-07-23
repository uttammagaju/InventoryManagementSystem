/// <reference path="salesreport.model.js" />

const mode = {
    create: 1,
    update: 2
};

var SalesReportController = function () {
    var self = this;
    const baseUrl = "/api/SalesReportAPI";
    self.CurrentOrder = ko.observableArray([]);
    self.CustomersNameList = ko.observableArray([]);
    self.ItemsNameList = ko.observableArray([]);
    self.IsUpdated = ko.observable(false);
    self.SelectedOrder = ko.observable(new SalesMasterVM({}, self));//self typically refers to the parent ViewModel or controller (SalesReportController).
    self.NewOrder = ko.observable(new SalesMasterVM({},self));
    self.mode = ko.observable(mode.create);

    // Fetch Data From Server 
    self.getData = function () {
        ajax.get(baseUrl + "/GetAll").then(function (result) {
            self.CurrentOrder(result.map(item => new SalesMasterVM(item,self)));
        });
    }
    self.getData();

    self.getCustomersName = function () {
        ajax.get(baseUrl + "/GetAllCustomers").then(function (result) {
            self.CustomersNameList(result.map(item => new CustomerNameVM(item)));
        });
    }
    self.getCustomersName();

    self.getItemsName = function () {
        ajax.get(baseUrl + "/GetItemsName").then(function (result) {
            self.ItemsNameList(result.map(item => new ItemNameVM(item)));
        });
    };

    self.getItemsName(); 
   


    self.AddOrder = function () {
        var orderData = ko.toJS(self.IsUpdated() ? self.SelectedOrder : self.NewOrder);
        console.log("Order Data before sending:", orderData); // Log the data

        //// Format the data if needed
        //var formattedOrder = {
        //    Id: orderData.Id,
        //    CustomerId: orderData.CustomerId,
        //    SalesDate: orderData.SalesDate,
        //    CustomerName: orderData.CustomerName,
        //    InvoiceNumber: orderData.InvoiceNumber,
        //    Discount: orderData.Discount,
        //    Sales: orderData.Sales.map(function (item) {
        //        return {
        //            Id: item.Id,
        //            ItemId: item.ItemId,
        //            Unit: item.Unit,
        //            Quantity: item.Quantity,
        //            Price: item.Price
        //        };
        //    })
        //};

        //console.log("Formatted Order Data:", formattedOrder); // Log the formatted data

        //// Validate the order data
        //var validation = self.validateOrder(formattedOrder);
        //if (!validation.isValid) {
        //    alert(validation.errorMessage);
        //    return;
        //}

        switch (self.mode()) {
            case mode.create:
                ajax.post(baseUrl + "/Create", JSON.stringify(orderData))
                    .done(function (result) {
                        if (result.success) {
                            console.log("Data received", result);
                            self.CurrentOrder.push(new SalesMasterVM(result, self));
                            self.resetForm();
                            self.getData();
                            $('#orderModal').modal('hide');
                        }
                        else {
                            alert(result.message)
                        }
                    })
                    .fail(function (err) {
                        console.log("Error adding order:", err);
                    });
                break;
            case mode.update:
                ajax.put(baseUrl + "/Update", JSON.stringify(formattedOrder))
                    .done(function (result) {
                        var updatedOrder = new SalesMasterVM(result, self);
                        var index = self.CurrentOrder().findIndex(function (item) {
                            return item.Id() === updatedOrder.Id();
                        });
                        if (index >= 0) {
                            self.CurrentOrder.replace(self.CurrentOrder()[index], updatedOrder);
                        }
                        self.resetForm();
                        self.getData();
                        $('#orderModal').modal('hide');
                    })
                    .fail(function (err) {
                        console.log("Error updating order:", err);
                    });
                break;
        }
    };

    //self.validateOrder = function (orderData) {
    //    var isValid = true;
    //    var errorMessage = "";

    //    if (!orderData.CustomerId) {
    //        isValid = false;
    //        errorMessage += "Customer is required. ";
    //    }

    //    if (!orderData.SalesDate) {
    //        isValid = false;
    //        errorMessage += "Date is required. ";
    //    }

    //    if (orderData.Sales.length === 0) {
    //        isValid = false;
    //        errorMessage += "At least one item is required. ";
    //    }

    //    orderData.Sales.forEach((item, index) => {
    //        if (!item.ItemId) {
    //            isValid = false;
    //            errorMessage += `Item is required for item ${index + 1}. `;
    //        }
    //        if (!item.Quantity || item.Quantity <= 0) {
    //            isValid = false;
    //            errorMessage += `Valid Quantity is required for item ${index + 1}. `;
    //        }
    //        if (!item.Price || item.Price <= 0) {
    //            isValid = false;
    //            errorMessage += `Valid Price is required for item ${index + 1}. `;
    //        }
    //    });

    //    return { isValid, errorMessage };
    //};


    // Delete Product
    self.DeleteProduct = function (model) {
        ajax.delete(baseUrl + "/Delete?id=" + model.Id())
            .done((result) => {
                self.CurrentOrder.remove(function (item) {
                    return item.Id() === model.Id();
                });
            }).fail((err) => {
                console.log(err);
            });
    };

    self.SelectOrder = function (model) {
        self.SelectedOrder(model);
        self.IsUpdated(true);
        self.mode(mode.update);
        $('#orderModal').modal('show');
    }

    self.CloseModel = function () {
        self.resetForm();
    }

    self.resetForm = function () {
        self.NewOrder(new SalesMasterVM({}, self));
        self.SelectedOrder(new SalesMasterVM({},self));
        self.IsUpdated(false);
       // self.AddItem(); // Add an initial empty item
    }

    // Remove Item
    self.RemoveItem = function (item) {
        if (self.IsUpdated()) {
            self.SelectedOrder().Sales.remove(item);
        } else {
            self.NewOrder().Sales.remove(item);
        }
    };

     //Add Item
    self.AddItem = function () {
        if (self.IsUpdated()) {
            self.SelectedOrder().Sales.push(new SalesDetailVM({},self));
        } else {
            self.NewOrder().Sales.push(new SalesDetailVM({},self));
        }
    };

    //self.totalAmount = ko.computed(function () {
    //    var items = self.IsUpdated() ? self.SelectedOrder().Items() : self.NewOrder().Items();
    //    var total = items.reduce(function (total, item) {
    //        var price = parseFloat(item.Price()) || 0;
    //        var quantity = parseInt(item.Quantity()) || 0;
    //        return total + (price * quantity);
    //    }, 0);
    //    return total.toFixed(2);
    //});


    self.calculateTotal = function () {
        self.totalAmount.notifySubscribers();  // Recalculate the total amount
    };

    // Initialize the form
    self.resetForm();
}

var ajax = {
    get: function (url) {
        return $.ajax({
            method: "GET",
            url: url,
            async: false,
        });
    },
    post: function (url, data) {
        return $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            method: "POST",
            url: url,
            data: data
        });
    },
    put: function (url, data) {
        return $.ajax({
            headers: {
                'Accept': 'application/json',
                'Content-Type': 'application/json'
            },
            method: "PUT",
            url: url,
            data: data
        });
    },
    delete: function (route) {
        return $.ajax({
            method: "DELETE",
            url: route,
        });
    }
};