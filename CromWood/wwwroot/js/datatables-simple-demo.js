window.addEventListener('DOMContentLoaded', event => {
    // Simple-DataTables
    // https://github.com/fiduswriter/Simple-DataTables/wiki

    const datatablesSimple = document.getElementById('datatablesSimple');
    if (datatablesSimple) {
        new simpleDatatables.DataTable(datatablesSimple);
    }

    const role_permissions = document.getElementById('role_permissions');
    if (role_permissions) {
        new simpleDatatables.DataTable(role_permissions);
    }


    // Assert details page tables
    const address_details = document.getElementById('address_details');
    if (address_details) {
        new simpleDatatables.DataTable(address_details);
    }

    const valuetion_table = document.getElementById('valuetion_table');
    if (valuetion_table) {
        new simpleDatatables.DataTable(address_details);
    }

    const finiancial_table = document.getElementById('finiancial_table');
    if (finiancial_table) {
        new simpleDatatables.DataTable(address_details);
    }

    const landloard_table = document.getElementById('landloard_table');
    if (landloard_table) {
        new simpleDatatables.DataTable(address_details);
    }
    // Assert details page tables end here

    // proparty detial tab page tabs name (property_condition)
    const property_condition = document.getElementById('property_condition');
    if (property_condition) {
        new simpleDatatables.DataTable(property_condition);
    }

    const Licensing_Certifications = document.getElementById('Licensing_Certifications');
    if (Licensing_Certifications) {
        new simpleDatatables.DataTable(Licensing_Certifications);
    }

    const hhsrs = document.getElementById('hhsrs');
    if (hhsrs) {
        new simpleDatatables.DataTable(hhsrs);
    }

    // Tenancy Management details page tabs
   
    const Recurring_Charges_tab = document.getElementById('Recurring_Charges_tab');
    if (Recurring_Charges_tab) {
        new simpleDatatables.DataTable(Recurring_Charges_tab);
    }

    const Payment_Plans = document.getElementById('Payment_Plans');
    if (Payment_Plans) {
        new simpleDatatables.DataTable(Payment_Plans);
    }
    
    const Complaints_Tab = document.getElementById('Complaints_Tab');
    if (Complaints_Tab) {
        new simpleDatatables.DataTable(Complaints_Tab);
    }

    const Messages___tab = document.getElementById('Messages___tab');
    if (Messages___tab) {
        new simpleDatatables.DataTable(Messages___tab);
    }

    const Unit_Utilities_tab = document.getElementById('Unit_Utilities_tab');
    if (Unit_Utilities_tab) {
        new simpleDatatables.DataTable(Unit_Utilities_tab);
    }
    
    // Recurring Charges active popup
    const  Charge_History_tab = document.getElementById('Charge_History_tab');
    if (Charge_History_tab) {
        new simpleDatatables.DataTable(Charge_History_tab);
    }
    
    const  Statement___tab = document.getElementById('Statement___tab');
    if (Statement___tab) {
        new simpleDatatables.DataTable(Statement___tab);
    }

    const Possession_Claims = document.getElementById('Possession_Claims');
    if (Possession_Claims) {
        new simpleDatatables.DataTable(Possession_Claims);
    }
    
    const Capital_CostForecast_Report = document.getElementById('Capital_CostForecast_Report');
    if (Capital_CostForecast_Report) {
        new simpleDatatables.DataTable(Capital_CostForecast_Report);
    }

    const st_action_table = document.getElementById('st_action_table');
    if (st_action_table) {
        new simpleDatatables.DataTable(st_action_table);
    }

    const Unit_reading_tab = document.getElementById('Unit_reading_tab');
    if (Unit_reading_tab) {
        new simpleDatatables.DataTable(Unit_reading_tab);
    }
   
    const scheduled_messages_table = document.getElementById('scheduled_messages_table');
    if (scheduled_messages_table) {
        new simpleDatatables.DataTable(scheduled_messages_table);
    }

    const Recent__Transactions_tab = document.getElementById('Recent__Transactions_tab');
    if (Recent__Transactions_tab) {
        new simpleDatatables.DataTable(Recent__Transactions_tab);
    }

    const RecAcces_table = document.getElementById('RecAcces_table');
    if (RecAcces_table) {
        new simpleDatatables.DataTable(RecAcces_table);
    }
    
});
