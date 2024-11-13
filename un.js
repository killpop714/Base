var num = 0;
const $btn = document.querySelector('#Checkbtn');
const $input = document.querySelector('#i1');
const $order = document.querySelector("#order");
const $word = document.querySelector("#word");

let data;//현재 단어
let word =[];//단어(제시어)
let submit = false;


const inputdata = (e) =>
{
    data= e.target.value
    console.log(data.length);
    console.log(word);

};

while(num<=0)
{
    num = Number(prompt("참가자는 몇명인가요?"));
    if(num<=0)
        console.log("다시 입력해주세요.");
}


const Output = () =>
 {

    if(!word  || word[word.length -1] === data[0] )// 제시어가 비어있거나 둘의 값이 같거나
    {
        word = data;
        $word.textContent = data;
        console.log("데이터가 저장되었습니다.");
        console.log( $word.textContent);
        const order = Number($order.textContent);
        if(order + 1>num){
            $order.textContent = 1;
        }
        else
        {
            $order.textContent = order + 1;
        }
        $input.value ="";
        $input.focus();
    }

    else
    {
        alert("잘못된 값이니다.");
        $input.value ="";
        $input.focus();
    }
                
    
}

$input.addEventListener('input', inputdata);
$btn.addEventListener('click',Output);


