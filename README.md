<h1 align="left">Domies API</h1>
<p>This is the backend API for Domies web application. Domies is a web application that supports hiring individuals as pet sitters for a specified period of time. 
  It offers a safe space that will connect pet owners and people willing to take care of their pets while their owners are away. It allows users to book pet sitters for a specific period of time. </p>


<h2 align="left">Technologies</h2>

<ul>
  <li><b>ASP .NET Core</b> - A cross-platform framework for building high-performance web applications, enabling efficient handling of HTTP requests and easy database integration.</li>
  <li><b>Entity Framework</b> - An ORM for .NET that simplifies database interactions, enabling easy data management and querying with reduced SQL code.</li>
  <li><b>SQL Server</b> - A relational database management system that ensures data integrity, scalability, and supports complex queries and transactions. </li>
</ul>

<h2 align="left">Installation</h2>
<ol>
  <li>Clone the repository.</li>
  <li>Create a new query window on the database server and run the <code>domies_skrypt.sql.</code></li>
  <li>Set up environment variables:</li>
  <ul>
    <li>Find a <code>.env</code> file in <code>DomiesAPI</code> folder.</li>
    <li>Update the <code>CONNECTION_STRING</code> value in the <code>.env</code> file to match your database name, and ensure the connection string reflects this change before starting the app.</li>
    <li>Ensure that <code>FRONTEND_URL</code> in your <code>.env</code> points  to the correct frontend address.</li>
  </ul>
  <li>Run the application.</li>
</ol>
