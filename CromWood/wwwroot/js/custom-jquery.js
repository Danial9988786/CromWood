
$(document).ready(function () {
  $(".datatable-table thead tr th button.datatable-sorter").append("<span class='c__icon'></span>");
  
  // For Select2 4.1
    $('.select_font_12').select2({
        theme: "bootstrap-5",
        placeholder: "Select...",
        width: '100%',
        dropdownCssClass: "select_font_12"
    });

    $('.modal__search').select2({
        theme: "bootstrap-5",
        placeholder: "Select...",
        width: '100%',
        dropdownCssClass: "modal__search",
        dropdownParent: ".ModalFixed",
    });
    $('.modal__search2').select2({
      theme: "bootstrap-5",
      placeholder: "Select...",
      width: '100%',
      dropdownCssClass: "modal__search2",
      dropdownParent: ".ModalFixed2",
   });
    
    $('.modal__search3').select2({
      theme: "bootstrap-5",
      placeholder: "Select...",
      width: '100%',
      dropdownCssClass: "modal__search3",
      dropdownParent: ".ModalFixed3",
    });
    
    $('.select__search').select2({
      theme: "bootstrap-5",
      placeholder: "Select...",
      width: '100%',
      dropdownCssClass: "select__search",
    });

    $('.select_font_14').select2({
        theme: "bootstrap-5",
        placeholder: "Select...",
        width: '100%',
        dropdownCssClass: "select_font_14"
    });
});

   // CK Editor library start here
   $(document).ready(function () { 
    ClassicEditor
    .create( document.querySelector('.CKeditor') )
    .catch( error => {
        console.error( error );
    });

    ClassicEditor
    .create( document.querySelector('.CKeditor2') )
    .catch( error => {
        console.error( error );
    });
  });

// ----------------   Steps form start here  ----------------
$(document).ready(function () {
  var currentIndex = 0;
    $(".star__r").click(function () {
        if ($(this).hasClass('fvrtd')) {
            $(this).removeClass('fvrtd');
        }
        else {
            $(this).addClass('fvrtd')
        }
    })
  $('.step_nextbtn').click(function () {
    // Remove the active class from the current item
    $('.step__f .step').eq(currentIndex).removeClass('active');
    $('.form___info form fieldset').eq(currentIndex).removeClass('active');

    // Increment the index or reset to 0 if it exceeds the number of items
    currentIndex = (currentIndex + 1) % $('.step__f .step').length;
    // currentIndex = (currentIndex + 1) % $('.form___info form fieldset').length;

    // Add the active class to the next item
    $('.step__f .step').eq(currentIndex).addClass('active');
    $('.step__f .step').eq(currentIndex).addClass('pass');
    $('.form___info form fieldset').eq(currentIndex).addClass('active');
   });

    $('.stepbackbtn').click(function () {
    // Remove the active class from the current item
    $('.step__f .step').eq(currentIndex).removeClass('active');
    $('.form___info form fieldset').eq(currentIndex).removeClass('active');

     // Decrement the index or set to the last item if it goes below 0
     currentIndex = (currentIndex - 1 + $('.step__f .step').length) % $('.step__f .step').length;

    // Add the active class to the previous item
    $('.step__f .step').eq(currentIndex).addClass('active');
    $('.form___info form fieldset').eq(currentIndex).addClass('active');
  });

    // Set the first items as active initially
    $('.step__f .step:first-child').addClass('active');
    $('.step__f .step:first-child').addClass('pass');
    // $('.step__f .step:last-child').removeClass('pass');
    $('.form___info form fieldset:first-child').addClass('active');

    // ----------------   Tooltip function called from here  ----------------
      tooltipPosition()
    // ----------------   Tooltip function called end here  ----------------
});
// ----------------   Steps form End here  ----------------

// ----------------   Tooltip function called Start from here  ----------------
  function tooltipPosition() {
    document.querySelectorAll(".sb-nav-link-icon").forEach(function(element) {
      element.addEventListener("mouseover", function() {
        var rect = this.getBoundingClientRect();
        var nextElement = this.nextElementSibling;
        var secondNextElement = nextElement.nextElementSibling;
        secondNextElement.style.top = (rect.top - 10) + "px";
      });
    });
    document.querySelectorAll(".dropdown_tooltip").forEach(function(element) {
      element.addEventListener("mouseover", function() {
        var rect = this.getBoundingClientRect();
        var tooltipElement = this.querySelectorAll(".tooltiptext")
        tooltipElement.forEach(function(toolChild) {
          toolChild.style.top = (rect.top) + "px";
        });
      });
    });
    }
// ----------------   Tooltip function called End from here  ----------------


// ---------    Top search bar text changing effact start here  ----------------
$(document).ready(function () {
  $('.t_search input[type="text"]').click(function () {
    $('.t_search>div').toggleClass('input_on');
   });
});
// ---------    Top search bar text changing effact End here  ----------------

// ---------    Add filter in after searhbar  ----------------
$(document).ready(function () {
  $(this).find(".NeedaFillter:has(.filter_btn)").addClass("F__Show");
   var MYfilter = $(this).find('.NeedaFillter.F__Show .filter_btn');
  
  $(this.F__Show).ready(function () {
     $(".datatable-top").append(MYfilter);
   });

});

// ---------  Add filter in after searhbar end here  ----------------

// For table, Show data in case when data go behind more then 10 row
  $(document).ready(function () {
    // Check the number of rows in the table
    const tables = $('.datatable-table')
    for (let index = 0; index < tables.length; index++) {
      const element = tables[index];
      const tableRows = $(element)[0].rows.length
      if (tableRows <= 10) {
        $(element).parent().next().hide()
      } 
    }
  });

// (End here)  For table, Show data in case when data go behind more then 10 row

// Show Hide Proparty details page (Proparty tab's Bage) only on Proparty tab


  // Quantity Plus Minus Js Start here
    $(document).ready(function() {
      $(".increment").on("click", function() {
        // Increment the value of the input
        incrementValue();
      });

      $(".decrement").on("click", function() {
        // Decrement the value of the input
        decrementValue();
      });

      function incrementValue() {
        var currentValue = parseInt($(".product_A_qty").val(), 10);
        $(".product_A_qty").val(currentValue + 1);
      }

      function decrementValue() {
        var currentValue = parseInt($(".product_A_qty").val(), 10);
        if (currentValue > 0) {
          $(".product_A_qty").val(currentValue - 1);
        }
      }
    });
  // Quantity Plus Minus Js END here


    // Date picker Custom JS stating here 
    $(function() {
      $('.datepicker').datepicker();
    });
    // Date picker Custom JS Ending here 

  //  --------  Massege Tab Js start here ----------------
      $('.Receipents_tab').click(function (e) {
        e.preventDefault()
        $('#Receipents-tab').tab('show');
        $(".Receip__btns").removeClass("d-none");
      });
    
      $('.bmc').click(function (e) {
        e.preventDefault()
        $('#Message_Contents-tab').tab('show');
        $(".Receip__btns").addClass("d-none");
        $(".MessageContents_btns").removeClass("d-none");
      });

      $("#Message_Contents-tab").click(function(){
        $(".MessageContents_btns").removeClass("d-none");
        $(".Receip__btns").addClass("d-none");
      });

      $("#Receipents-tab").click(function(){
        $(".MessageContents_btns").addClass("d-none");
        $(".Receip__btns").removeClass("d-none");
      });
    //  --------  Massege Tab Js start End here ----------------


    // password hide show funtionalty on eye icon
    $(document).ready(function() {
        $(".pswEye").click(function () {
          var inputPass = $(this).prev('input');
        if (inputPass.attr("type") === "password") {
          inputPass.attr("type", "text");
        } else {
          inputPass.attr("type", "password");
        }
      })
    });

  $(document).ready(function() {
      // If empty, add a new image in table
      $('.datatable-table tbody tr td.datatable-empty')
      .html('<div class="emptyMSG"><img class="mb-3" src="./assets/img/noDatafound.svg" alt="noDatafound Icon"><p class="sora_16 mb-0">no data found!</p></div>');
  });
    

  // show hide text of input on focus (Page Condition assessment details)
    $('.ItemSer').focus(function(){
      $(this).next().show();
    });
    $('.ItemSer').blur(function(){
      $(this).next().hide();
    });

  // Housing_Items Jquery
  $('.condition__item li').click(function(){
    $('.condition__item li').removeClass("active");
    $(this).addClass('active');
    $('.condition__item li').removeClass("DONE");
    $(this).prev('li').addClass('DONE');
  });

  $('.condition__item li').click(function(){
      var custom_index = 0;
      var custom_index = $(this).parents().find('.assessment_item').eq(custom_index + 1);
      $(custom_index).find('.Housing_Items').addClass('active');
      $('.assessment_item.second2 .amptyImg_box').hide();
    })

   $('.assessment_item.second2 .condition__item li').click(function(){
      var custom_index2 = $(this).parents().find('.assessment_item.second2').next();
      $(custom_index2).find('.Housing_Items').addClass('active');
      $('.assessment_item.third3').show();
   })

    // Add conditon BTN Jquery 
  $('.addCondition_Btn').one('click', function(){
   $(this).next().addClass('active');
  });


  // Mobile menu icon Jquery
  $(".MobIcon").click(function(){
    $("#layoutSidenav #layoutSidenav_nav").toggleClass("MobMenuActive");
  });
  $(".MobClose").click(function(){
    $("#layoutSidenav #layoutSidenav_nav").removeClass("MobMenuActive");
  });
  
    //  Media addClass for Mobile view to make responsive
    $(document).ready(function($) {
      var alterClass = function() {
        var ww = document.body.clientWidth;
        if (ww < 992) {
          $('body.sb-nav-fixed').removeClass('sb-sidenav-toggled');
         } 
       };
  
        $(window).resize(function(){
          alterClass();
        });
        //Fire it when the page first loads:
        alterClass();
     });
  


    // custom selec option Jquery start from here ------------------
    $(document).ready(function() {
      $('.customSelect').each(function() {
        var $select = $(this);
        var $selectedOption = $select.find('.selected-option');
        var $options = $select.find('.options');
        var $hiddenInput = $select.find('.hidden-input');
    
        // Toggle options on click
        $selectedOption.on('click', function() {
          $options.toggle();
        });
    
        // Handle option selection
        $options.on('click', 'li', function() {
          var value = $(this).data('value');
          $selectedOption.text($(this).text());
          $hiddenInput.val(value);
          $options.hide();
        });
    
        // Close options when clicking outside
        $(document).on('click', function(event) {
          if (!$select.is(event.target) && $select.has(event.target).length === 0) {
            $options.hide();
            $('.customSelect').removeClass('on');
          }
        });
      });
    });

    $(document).ready(function() {
       $('.customSelect').click(function(){
         $(this).toggleClass("on");
       });
    });
    
   // custom selec option Jquery END from here  ------------------------
     
   

