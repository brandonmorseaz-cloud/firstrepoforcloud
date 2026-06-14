document
    .getElementById("contactForm")
    .addEventListener("submit", sendMessage);

async function sendMessage(event)
{
    event.preventDefault();

    const data = 
    {
        name: document.getElementById("name").value,
        email: document.getElementById("email").value,
        message: document.getElementById("message").value
    };

    try
    {
        const response = await fetch(
            "http://localhost:7071/api/ContactForm",
            {
                method: "POST",
                headers:
                {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(data)
            });

            const result = await response.text();

            if(response.ok)
            {
                document.getElementById("status").innerText = 
                result;
            }
            else
            {
                document.getElementById("status").innerText =
                `Failed: ${result}`;
            }
        }
            catch(error)
            {
                document.getElementById("status").innerText = 
                "ERROR OCCURRED!";
            }
        }