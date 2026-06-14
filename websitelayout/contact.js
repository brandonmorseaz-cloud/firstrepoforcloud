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
            "PLACEHOLDER_AZUREFUNCTIONURL",
            {
                method: "POST",
                headers:
                {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify(data)
            });

            if(response.ok)
            {
                document.getElementById("status").innerText = 
                "Message sent!";
            }
            else
            {
                document.getElementById("status").innerText =
                "Failed to send...";
            }
        }
            catch(error)
            {
                document.getElementById("status").innerText = 
                "ERROR OCCURRED!";
            }
        }