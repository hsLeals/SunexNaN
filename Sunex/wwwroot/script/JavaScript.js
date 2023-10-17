$(".flagt").click(function () {
    $(".t").show();
    $(".a").hide();
})
$(".flaga").click(function () {
    $(".a").show();
    $(".t").hide();
})



$(".fa-bars").click(function () {
    $(".media-nav").show();
})
$(".fa-x").click(function () {
    $(".media-nav").hide();
})


$(".tracking a").click(function () {
    $(this).parent().parent().parent().children(".modal").show();
})






$(".cheki").keyup(function () {
    let a = this.value;
    if (a == 0.1) {
        document.querySelector(".result p").innerText = "Cəmi: 1$"
    } else if (a >= 0.2 && a <= 0.5) {
        document.querySelector(".result p").innerText = "Cəmi: 2$"
    } else if (a > 0.5 && a <= 0.7) {
        document.querySelector(".result p").innerText = "Cəmi: 3$"
    } else if (a > 0.7 && a <= 1) {
        document.querySelector(".result p").innerText = "Cəmi: 4$"
    } else if (a > 1) {
        let kesr = Math.abs(this.value - Math.round(this.value));
        let c = 1 - kesr;
        let tam = this.value - c;

        console.log(kesr);
        console.log(c);
        console.log(tam);

        if (kesr == 0.1) {
            let qiymet = "Qiymət" + 4 * tam + 1 + "$"
            document.querySelector(".result p").innerText = qiymet
        } else if (a >= 0.2 && a <= 0.5) {
            document.querySelector(".result p").innerText = "Cəmi: 2$"
        } else if (a > 0.5 && a <= 0.7) {
            document.querySelector(".result p").innerText = "Cəmi: 3$"
        } else if (a > 0.7 && a <= 1) {
            document.querySelector(".result p").innerText = "Cəmi: 4$"
        }

    } else if (this.value == 0) {
        document.querySelector(".result p").innerText = "Cəmi: 0$"
    }
});


function removeRow(x) {
    x.parentElement.remove()
    sirala()
}


let index = 0;
document.querySelector("#addrow").addEventListener('click', function () {
    index++;
    let p = document.createElement('div')
    p.className = "product"
    let inptName = document.createElement("input");
    inptName.name = `[${index}].ProductName`;
    inptName.placeholder = "Sifarişin adı"
    let inptLink = document.createElement('input');
    inptLink.name = `[${index}].ProductUrl`;
    inptLink.placeholder = "Link-i daxil edin"
    let inptCount = document.createElement("input");
    inptCount.type = "number";
    inptCount.name = `[${index}].ProductCount`;
    inptCount.placeholder = "Say"
    let btn = document.createElement('button')
    btn.type = "button";
    btn.innerText = "sil"
    btn.onclick = function () {
        this.parentElement.remove()
        sirala()
    }
    p.appendChild(inptName);
    p.appendChild(inptLink);
    p.appendChild(inptCount);
    p.appendChild(btn);
    document.querySelector('.products').append(p)
})

function sirala() {
    for (var i = 0; i < document.querySelectorAll('.product').length; i++) {
        document.querySelectorAll('.product')[i].querySelectorAll('input')[0].setAttribute("name",`[${i}].ProductName`)
        document.querySelectorAll('.product')[i].querySelectorAll('input')[1].setAttribute("name", `[${i}].ProductUrl`)
        document.querySelectorAll('.product')[i].querySelectorAll('input')[2].setAttribute("name", `[${i}].ProductCount`)
    }
}
