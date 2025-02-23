# Colloquium-11-CTF

## Flag Heist: A Multiplayer Capture the Flag Experience

### Overview
Flag Heist is a dynamic multiplayer game developed for Colloquium11 at Thapar University. Designed as a Capture the Flag challenge interwoven with cypher-solving tasks, the project uniquely blends fast-paced action with strategic puzzles. The game features two rounds:
- **Round 1 (FPS):** An encounter filled with various enemy types, including machine learning-driven adversaries.
- **Round 2 (Third-Person):** A non-combat, puzzle-focused environment.

### Technologies & Tools
- **Unity & Photon:** Core frameworks for game development and real-time multiplayer networking. *(Note: Photon PUN2’s free plan supports up to 20 concurrent users; multiple project IDs were created for the event.)*
- **Unity Assets:** Leveraged pre-built assets such as the FPS Microgame.
- **WebGL & Vercel:** Built in WebGL and hosted on Vercel for broad accessibility.
- **Google Cloud Platform (GCP):** Utilized a VM to manage session data, storing team scores and tracking answered questions for recovery in case of crashes.
- **Django:** Developed a leaderboard interface integrated with the GCP backend to display live scores.

### Key Features & Gameplay
- **Dual Gameplay Rounds:**
  - **Round 1 (FPS):**
    - Fast-paced combat with various enemy types.
    - Scoring: 100 points per question, with a 30-point deduction for every hint and an extra 10-point deduction for dying or falling off the map.
  - **Round 2 (Third-Person):**
    - Puzzle-oriented gameplay.
    - Scoring: 200 points per question, with a 30-point penalty for each hint.
- **Real-Time Multiplayer:** Enabled team-based challenges via Photon networking.
- **Data Persistence & Recovery:** Continuous session data saving mitigated network instability issues.
- **Leaderboard Integration:** A Django-powered leaderboard tracked team performance. Although it crashed mid-event due to high request volumes, the issue was managed manually.

### Pre-Event Preparations & Challenges
- **WebGL Copy-Paste Functionality:**  
  Proactively addressed before the event by integrating an open-source solution from Greggman’s Unity WebGL Copy and Paste repository.
- **Leaderboard Stability:**  
  During the event, high concurrent requests led to leaderboard crashes (exacerbated by Photon PUN2's limitations). Multiple Photon project IDs were used, and the issue was handled manually.
- **Security Considerations:**  
  REST API endpoints posed potential vulnerabilities for unauthorized score modifications or data deletions; fortunately, no malicious activity occurred.

### Event Impact
With participation from over 15 teams, Flag Heist delivered an engaging and challenging experience. The project showcased our ability to rapidly integrate diverse technologies while providing valuable insights into live event management, data resilience, and security in real-time multiplayer environments.

### Conclusion
Flag Heist stands as a testament to innovative problem-solving and collaborative development. By addressing pre-event challenges and managing unforeseen issues during the event, this project has contributed valuable lessons in network management, cross-platform integration, and scalable web services—driving future advancements in real-time game development.
=======
# Colloquium11-CTF
Flag Heist is a Capture the Flag game for Colloquium11 at Thapar University. It has two rounds: an FPS round with ML enemies (100-point questions with penalties) and a third-person round with 200-point puzzles. Built with Unity, Photon (multiple IDs), hosted on Vercel with a GCP VM and a Django leaderboard.
