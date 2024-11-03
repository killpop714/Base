let input = new Array();
let rand;
let tries =[];


function checkInput(input)
{
    //길이가 4이하인가?
    if(input.length !==4){
        return alert("4자리 숫자를 입력해 주세요.")
    }
    //중복된 숫자가 있는가?
    if(new Set(input).size!==4){
        return alert("중복되지 않게 입력해 주세요.")
    }

    if(tries.includes(input)){
        return alert("이미 시도한 값입니다.")
    }
    return true;
}


var $form = document.getElementById("form");
var $input = document.querySelector("input");
var $btn = document.getElementById("btn");
var $logs = document.getElementById("logs");

$form.addEventListener('submit',(event)=>{
    event.preventDefault();
    $input.value = checkInput(value) {
        $logs.textContent ="홈런";
        return;
    }
})

//테스트 확인중
//변화
//시스템 커밋
//변화

//커밋 데이터