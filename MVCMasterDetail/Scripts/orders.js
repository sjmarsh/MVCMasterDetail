$("#addOrderItem").on('click', function () {
  $.ajax({
    async: false,
    url: '/Order/CreateOrderItem'
  }).success(function (partialView) {
    $('#orderItems table tbody:last-child').append(partialView);
  });
});

$("a.deleteRow").on("click", function () {
  $(this).closest('#OrderItem').remove();
  return false;
});