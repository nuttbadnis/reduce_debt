
$('#btn_flow_submit').click(function() {
	$('input[xd="hide_flow_no"]').val($('#flow_no').val());
	$('input[xd="hide_flow_sub"]').val($('#flow_sub').val());
	$('input[xd="hide_next_step"]').val($('#next_step').val());
	$('input[xd="hide_back_step"]').val($('#back_step').val());
	$('input[xd="hide_department"]').val($('#department').val());
	//$('input[xd="hide_flow_status"]').val($('#sel_flow_status').val());
	$('input[xd="hide_flow_remark"]').val($('#txt_flow_remark').val());
	$('input[xd="hide_project_code"]').val($('#txt_flow_projectcode').val());
	//$('input[xd="hide_contract_file"]').val($('#flow_file').val());

	var $el1 = $("#sel_flow_status");
	//var $rbtApprove = $("#rbtApprove");
	
	if ($el1.val() == 99) {
	    var $rbtApprove = $("#rbtApprove");
	    var $rbtNotApprove = $("#rbtNotApprove");
	    if (document.getElementById('rbtApprove').checked) {
	        $el1 = $rbtApprove
	    }
	    if (document.getElementById('rbtNotApprove').checked) {
	        $el1 = $rbtNotApprove
	    }
	}

    $('input[xd="hide_flow_status"]').val($el1.val());

	if ($el1.val() == 30 && $.trim($('#txt_flow_remark').val()).length == 0) {
		var txt = "กรุณากรอกหมายเหตุที่ต้องการข้อมูลเพิ่ม"
		//modalAlert(txt);
		//$('#modal_alert').on('hidden.bs.modal', function (e) {
		//	$('#txt_flow_remark').focus();
		//})
		alert(txt);
		$('#txt_flow_remark').focus();
	}
	else if ( ($el1.val() == 55 || $el1.val() == 65) && $.trim($('#txt_flow_remark').val()).length == 0) {
		var txt = "กรุณากรอกหมายเหตุที่ไม่อนุมัติ/ไม่ดำเนินการ"
		//modalAlert(txt);
		//$('#modal_alert').on('hidden.bs.modal', function (e) {
		//	$('#txt_flow_remark').focus();
		//})
		alert(txt);
		$('#txt_flow_remark').focus();
	}
	else if ($.trim($el1.val()) != "" && $el1.val() != 50 && $el1.val() != 60) {
		// ถ้าเลือกสถานะอื่นที่ไม่ใช่ อนุมัติ และดำเนินการเรียบร้อย สามารถ submit ได้เลย
		$('input[xd="btn_flow_hidden"]').click();
	}
	else if ($('#department').val() == 8 && $el1.val() == 50 && $.trim($('#txt_flow_projectcode').val()).length == 0) {
	    alert("กรุณากรอกข้อมูล Project Code");
		$('#txt_flow_projectcode').focus();
	}
	else if ($('#department').val() == 0 && $el1.val() == 50 && $.trim($('#flow_file').val()).length < 5) {
	    alert("กรุณาแนบ เอกสารสัญญา เป็นไฟล์ PDF เท่านั้น");
		$('#flow_file').focus();
	}
	else if ($('#department').val() == 0 && $el1.val() == 50 && $.trim($('#flow_file').val()).substring($.trim($('#flow_file').val()).length - 4, $.trim($('#flow_file').val()).length) != '.pdf') {
	    //var iLen = $.trim($('#flow_file').val()).length;
	    //if (iLen >= 3) {
	    //    if ($.trim($('#flow_file').val()).substring(iLen - 3, iLen) != 'pdf') {
	    //        alert("กรุณาแนบ เอกสารสัญญา เป็นไฟล์ PDF เท่านั้น");
	    //    } else{
	    //        alert("Complete");
	    //    }
	    //} else {
	    //    alert("กรุณาแนบ เอกสารสัญญา เป็นไฟล์ PDF เท่านั้น");
	    //}
	    //alert($('#flow_file').val());
	    alert("กรุณาแนบ เอกสารสัญญา เป็นไฟล์ PDF เท่านั้น");
		$('#flow_file').focus();
	}
	else if (!checkSubmit('required-f')) {
		var txt = "กรุณากรอกข้อมูลให้ครบถ้วน" //"หากต้องการอนุมัติ หรือดำเนินการเรียบร้อย <br>กรุณากรอกข้อมูลช่องที่มีเครื่องหมาย <span class='txt-red'>*</span> <br><b>ในฟอร์มอนุมัติให้ครบ เพื่อบันทึก</b>"
		//modalAlert(txt);
		//$('#modal_alert').on('hidden.bs.modal', function (e) {
		//	$('.error:first').focus();
		//})
		alert(txt);
	}
	else{
		$('input[xd="btn_flow_hidden"]').click();
	}
});

$('#btn_add_next_submit').click(function() {
	$('input[xd="hide_flow_no"]').val($('#flow_no').val());
	$('input[xd="hide_flow_sub"]').val($('#flow_sub').val());
	$('input[xd="hide_depart_id"]').val($('#sel_depart_id').val());

	if (!checkSubmit('required-n')) {
		modalAlert("กรุณาเลือกส่วนงาน เพื่อแทรกลำดับถัดไป");
		$('#modal_alert').on('hidden.bs.modal', function (e) {
			$('.error:first').focus();
		})
	}
	else{
		$('input[xd="btn_add_next_hidden"]').click();
	}
});

