var num = 0;
const $btn = document.querySelector('#Checkbtn');
const $input = document.querySelector('#i1');
const $order = document.querySelector("#order");
const $word = document.querySelector("#word");
const $time = document.querySelector("#time");

const  Data = ["가성성","가루집","나노관","나무람","나트륨","나팔꼿"]
let UseWord = [];

let data;//현재 단어
let word;//단어(제시어)


const Int = setInterval(TimeOut,1000);
let timesec=-1;



while(isNaN(num) || num<1 || !num)
    {
        num = Number(prompt("참가자는 몇명인가요?"));
        if(num<=0)
            console.log("다시 입력해주세요.");
    }

$word.textContent = "1번째 순서입니다.";

const inputdata = (e) =>
{
    data= e.target.value
    console.log(data);

};

const Output = () =>
 {


    if(!word && data.length === 3 || data.length === 3 && word[word.length -1], data[0] && Data.includes(data.toLowerCase()) && UseWord.includes(data.toLowerCase()) == false)
    {
        word = data;
        $word.textContent = data;
        UseWord.push(data);
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
        alert("중복 또는 잘못된 값이니다.");
        $input.value ="";
        $input.focus();
    }
    timesec =-1;
    Int;
             
    
}

function TimeOut()
{
    ++timesec;
    $time.textContent = timesec;
    if(timesec == 5)
    {
        setTimeout(()=>
        {
        alert("시간 아웃")
        $input.value ="";
        $input.focus();
        timesec =-1;
        },500)

    }
}

$input.addEventListener('input', inputdata);
$btn.addEventListener('click',Output);


