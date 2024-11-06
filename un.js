const num = Number(prompt("참가자는 몇명인가요?"));
var check =0;

const Andata = new Array(num);


const $input = document.querySelector('#i1');
const $btn = document.querySelector('#Checkbtn');
const $spam = document.querySelector("#spam");
const $order = document.querySelector("#order");

let data;
let word;
const inputdata = (e) =>
{
    data= e.target.value

};

    
const Output = () =>
 {
    if(num != check)
    {
        if(!word)
        {
            $spam.textContent = word;
            console.log("데이터가 저장되었습니다.");
            console.log( $spam.textContent);
            const order = Number($order.textContent);
            if(order + 1>number){
                $order.textContent = 1;
            }
            else
            {
                $order.textContent = order + 1;
            }
            check++;
        }
        else
        {
            if(word[word.length -1] === data[0])
            {
                $spam.textContent = word;
                check++;
            }
            else
            console.log("잘못된 값입니다.");
        }
                
    }
    else
    {
        console.log("데이터가 가득합니다.")
    }
}

$input.addEventListener('input', inputdata);
$btn.addEventListener('click',Output);


